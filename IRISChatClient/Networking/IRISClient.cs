using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using IRISChatClient.Configs;
using IRISChatClient.Helpers;
using IRISChatClient.Interfaces;
using IRISChatClient.Networking.Encryption;
using IRISChatClient.Networking.MessageManagement;
using IRISChatClient.Networking.Messages;

namespace IRISChatClient.Networking
{
    public class IRISClient
    {
        #region "Fields"
        private StreamSocket clientSocket;
        private string host;
        private string port;
        private bool attemptToReconnect;
        private bool isConnected;
        private bool isDisconnected;
        private byte[] headerBuffer;
        private byte[] messageBuffer;
        #endregion

        #region "Properties"
        public StreamSocket ClientSocket
        {
            get
            {
                return clientSocket;
            }
        }
        public string Host
        {
            get
            {
                return host;
            }
        }
        public string Port
        {
            get
            {
                return port;
            }
        }
        public IPEndPoint EndPoint
        {
            get
            {
                return new IPEndPoint(IPAddress.Parse(Host), Convert.ToInt32(Port));
            }
        }
        public bool AttemptToReconnect
        {
            get
            {
                return attemptToReconnect;
            }
        }
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
        }
        public bool IsDisconnected
        {
            get
            {
                return isDisconnected;
            }
        }
        #endregion

        #region "Events / Handlers"
        public delegate void OnClientConnectedEvent(IRISClient Client);
        public event OnClientConnectedEvent OnClientConnected;
        private void SetOnClientConnected()
        {
            OnClientConnected?.Invoke(this);
        }

        public delegate void OnClientSendEvent(IRISClient Client, IMessage Message);
        public event OnClientSendEvent OnClientSend;
        private void SetOnClientSend(IMessage Message)
        {
            OnClientSend?.Invoke(this, Message);
        }

        public delegate void OnClientReceiveEvent(IRISClient Client, IMessage Message);
        public event OnClientReceiveEvent OnClientReceive;
        private void SetOnClientReceive(IMessage Message)
        {
            OnClientReceive?.Invoke(this, Message);
        }

        public delegate void OnClientDisconnectEvent();
        public event OnClientDisconnectEvent OnClientDisconnect;
        private void SetOnClientDisconnect()
        {
            OnClientDisconnect?.Invoke();
        }

        public delegate void OnClientExceptionEvent(IRISClient Client, Exception ex);
        public event OnClientExceptionEvent OnClientException;
        private void SetOnClientException(Exception ex)
        {
            OnClientException?.Invoke(this, ex);
        }
        #endregion

        #region "Constructors"
        public IRISClient(string host, string port, bool attemptToReconnect)
        {
            this.host = host;
            this.port = port;
            this.attemptToReconnect = attemptToReconnect;
            Initialize();
        }
        #endregion

        #region "Private Methods"
        private void Initialize()
        {
            clientSocket = new StreamSocket();
            isConnected = false;
            isDisconnected = false;
            headerBuffer = new byte[Constants.HEADER_SIZE];
        }

        private async void ReceiveIncomingMessages()
        {
            try
            {
                //Create our buffer reader
                DataReader BufferReader = new DataReader(clientSocket.InputStream);
                //Load header bytes into the internal buffer.
                await BufferReader.LoadAsync(Constants.HEADER_SIZE);
                //Read the header bytes from the internal buffer and store them into the header buffer.
                BufferReader.ReadBytes(headerBuffer);
                //Convert the header buffer into message size.
                int MessageSize = BitConverter.ToInt32(headerBuffer, 0);

                //Load message bytes into the internal buffer.
                await BufferReader.LoadAsync((uint)MessageSize);
                //Allocate the message buffer with the message size if its null or resize it with the message size.
                if (messageBuffer == null)
                {
                    messageBuffer = new byte[MessageSize];
                }
                else
                {
                    Array.Resize(ref messageBuffer, MessageSize);
                }
                //Read the message bytes from the internal buffer and store them into the message buffer.
                BufferReader.ReadBytes(messageBuffer);

                //Process the message buffer.
                IMessage Message = ProcessMessage.Process(messageBuffer);
                //If Message is not null then we will inform the listeners otherwise disconnect.
                if (Message != null)
                {
                    SetOnClientReceive(Message);
                }
                else
                {
                    //The Message type is unknown means that it's not registered, source can be from attacker or unknown source.
                    SetOnClientException(new Exception("Unknown message type, source can be from attacker or unknown source."));
                    Disconnect();
                }
                //Recursion call to read back again if its connected.
                if (isConnected == true && isDisconnected == false)
                {
                    ReceiveIncomingMessages();
                }
            }
            catch (Exception ex)
            {
                SetOnClientException(ex);
                Disconnect();
            }
        }
        private async void Reconnect()
        {
            while (true)
            {
                try
                {
                    Log(string.Format("Attempting to reconnect, IsConnected: {0}", isConnected));
                    if (isConnected == false)
                    {
                        if (isDisconnected == true)
                        {
                            clientSocket = new StreamSocket();
                        }
                        await clientSocket.ConnectAsync(new HostName(Host), port);
                        SetOnClientConnected();
                        isConnected = true;
                        isDisconnected = false;
                        ThreadPool.QueueUserWorkItem(o => ReceiveIncomingMessages());
                        Log(string.Format("Connected successfully, IsConnected: {0}", isConnected));
                    }
                }
                catch (Exception ex)
                {
                    Log("Failed to reconnet, attempting to reconnect again");
                    SetOnClientException(ex);
                    Disconnect();
                }
                Thread.Sleep(Constants.RECONNECT_DELAY);
            }
        }
        #endregion

        #region "Public Methods"
        public async void Connect()
        {
            try
            {
                if (attemptToReconnect)
                {
                    new Thread(new ThreadStart(Reconnect)).Start();
                }
                else
                {
                    await clientSocket.ConnectAsync(new HostName(Host), port);
                    SetOnClientConnected();
                    isConnected = true;
                    ThreadPool.QueueUserWorkItem(o => ReceiveIncomingMessages());
                }
            }
            catch (Exception ex)
            {
                SetOnClientException(ex);
            }
        }

        public async void SendMessage(MessageWrapper Message)
        {
            try
            {
                DataWriter BufferWriter = new DataWriter(clientSocket.OutputStream);
                byte[] Packet = AES256.Encrypt(JsonConvert.SerializeObject(Message));
                byte[] MessageToSend = SocketHelper.AppendHeader(Packet);
                BufferWriter.WriteBytes(MessageToSend);
                await BufferWriter.StoreAsync();
                BufferWriter.DetachStream();
                BufferWriter.Dispose();
                SetOnClientSend((IMessage)Message.Message);
            }
            catch (Exception ex)
            {
                SetOnClientException(ex);
                Disconnect();
            }
        }

        public async void Disconnect()
        {
            try
            {
                if (isConnected && isDisconnected == false)
                {
                    isConnected = false;
                    isDisconnected = true;
                    SetOnClientDisconnect();
                    await clientSocket.CancelIOAsync();
                    clientSocket.InputStream.Dispose();
                    clientSocket.OutputStream.Dispose();
                    clientSocket.Dispose();
                }
            }
            catch (Exception ex)
            {
                SetOnClientException(ex);
            }
        }
        #endregion

        #region "Logger"
        private void Log(string Message)
        {
            Debug.WriteLine(Message);
        }
        #endregion
    }
}
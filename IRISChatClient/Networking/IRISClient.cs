using System;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
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
using System.Threading.Tasks;

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
        private List<Response> responseList;
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
            private set
            {
                attemptToReconnect = value;
            }
        }
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            private set
            {
                isConnected = value;
                if (value == true)
                {
                    SetOnClientStateChanged(true);
                }
            }
        }
        public bool IsDisconnected
        {
            get
            {
                return isDisconnected;
            }
            private set
            {
                isDisconnected = value;
                if (value)
                {
                    SetOnClientStateChanged(false);
                }
            }
        }
        #endregion

        #region "Events / Handlers"
        public delegate void OnClientStateChangedEvent(bool State);
        public event OnClientStateChangedEvent OnClientStateChanged;
        private void SetOnClientStateChanged(bool State)
        {
            OnClientStateChanged?.Invoke(State);
        }

        public delegate void OnClientAttemptToReconnectEvent();
        public event OnClientAttemptToReconnectEvent OnClientAttemptToReconnect;
        private void SetOnClientAttemptToReconnect()
        {
            OnClientAttemptToReconnect?.Invoke();
        }

        public delegate void OnClientConnectedEvent();
        public event OnClientConnectedEvent OnClientConnected;
        private void SetOnClientConnected()
        {
            OnClientConnected?.Invoke();
        }

        public delegate void OnClientSendEvent(IMessage Message);
        public event OnClientSendEvent OnClientSend;
        private void SetOnClientSend(IMessage Message)
        {
            OnClientSend?.Invoke(Message);
        }

        public delegate void OnClientReceiveEvent(IMessage Message);
        public event OnClientReceiveEvent OnClientReceive;
        private void SetOnClientReceive(IMessage Message)
        {
            OnClientReceive?.Invoke(Message);
        }

        public delegate void OnClientDisconnectEvent();
        public event OnClientDisconnectEvent OnClientDisconnect;
        private void SetOnClientDisconnect()
        {
            OnClientDisconnect?.Invoke();
        }

        public delegate void OnClientExceptionEvent(Exception ex);
        public event OnClientExceptionEvent OnClientException;
        private void SetOnClientException(Exception ex)
        {
            OnClientException?.Invoke(ex);
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
            IsConnected = false;
            IsDisconnected = false;
            headerBuffer = new byte[Constants.HEADER_SIZE];
            responseList = new List<Response>();
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
                    if (responseList.Count == 1)
                    {
                        Response CurrentResponse = responseList[0];
                        if (CurrentResponse.ExpectedMessageType.Equals(Message.GetType()))
                        {
                            CurrentResponse.Result = Message;
                            CurrentResponse.LastSeen = DateTime.Now;
                        }
                        else
                        {
                            CurrentResponse.IsTimedout = true;
                        }
                        CurrentResponse.Handler.Set();
                        CurrentResponse.Handler.Dispose();
                        responseList.RemoveAt(0);
                    }
                    else
                    {
                        SetOnClientReceive(Message);
                    }
                }
                else
                {
                    //The Message type is unknown means that it's not registered, source can be from attacker or unknown source.
                    SetOnClientException(new Exception("Unknown message type, source can be from attacker or unknown source."));
                    Disconnect();
                }
                //Recursion call to read back again if its connected.
                if (IsConnected == true && IsDisconnected == false)
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
                    Log(string.Format("Attempting to reconnect, IsConnected: {0}", IsConnected));
                    
                    if (IsConnected == false)
                    {
                        SetOnClientAttemptToReconnect();
                        if (IsDisconnected)
                        {
                            clientSocket = new StreamSocket();
                        }
                        await clientSocket.ConnectAsync(new HostName(Host), port);
                        IsConnected = true;
                        IsDisconnected = false;
                        new Thread(new ThreadStart(ReceiveIncomingMessages)).Start();
                        new Thread(new ThreadStart(ResponseMonitor)).Start();
                        SetOnClientConnected();
                        Log(string.Format("Connected successfully, IsConnected: {0}", IsConnected));
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

        private void ResponseMonitor()
        {
            Log(string.Format("Response Monitor started. IsConnected: {0}, IsDisconnected: {1}, ResponseCount: {2}", IsConnected, IsDisconnected, responseList.Count));
            while (IsConnected && !IsDisconnected)
            {
                Log(string.Format("Response Monitor is running. IsConnected: {0}, IsDisconnected: {1}, ResponseCount: {2}", IsConnected, IsDisconnected, responseList.Count));
                foreach (Response response in responseList.ToArray())
                {
                    if ((DateTime.Now - response.LastSeen).TotalSeconds >= Constants.RESPONSE_TIME_OUT)
                    {
                        Log(string.Format("Response Monitor detected timed out response. LastSeen: {0}, ExpectedMessageType: {1}, ResponseCount: {2}", response.LastSeen, response.ExpectedMessageType.Name, responseList.Count));
                        response.IsTimedout = true;
                        response.Handler.Set();
                        response.Handler.Dispose();
                        if (responseList.Remove(response) == false)
                        {
                            Log(string.Format("Response Monitor detected timed out response but failed to remove. LastSeen: {0}, ExpectedMessageType: {1}, ResponseCount: {2}", response.LastSeen, response.ExpectedMessageType.Name, responseList.Count));
                        }
                        else
                        {
                            Log(string.Format("Response Monitor detected timed out response but succeeded to remove. LastSeen: {0}, ExpectedMessageType: {1}, ResponseCount: {2}", response.LastSeen, response.ExpectedMessageType.Name, responseList.Count));
                        }
                    }
                }
                Thread.Sleep(Constants.RESPONSE_MONITOR_INTERVAL);
            }
            Log(string.Format("Response Monitor stopped. IsConnected: {0}, IsDisconnected: {1}, ResponseCount: {2}", IsConnected, IsDisconnected, responseList.Count));
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
                    IsConnected = true;
                    IsDisconnected = false;
                    new Thread(new ThreadStart(ReceiveIncomingMessages)).Start();
                    new Thread(new ThreadStart(ResponseMonitor)).Start();
                    SetOnClientConnected();
                }
            }
            catch (Exception ex)
            {
                SetOnClientException(ex);
            }
        }

        public async void SendMessage(IMessage Message)
        {
            try
            {
                if (IsConnected)
                {
                    MessageWrapper WrappedMessage = new MessageWrapper(Message.GetType().Name, Message);
                    DataWriter BufferWriter = new DataWriter(clientSocket.OutputStream);
                    byte[] Packet = AES256.Encrypt(JsonConvert.SerializeObject(WrappedMessage));
                    byte[] MessageToSend = SocketHelper.AppendHeader(Packet);
                    BufferWriter.WriteBytes(MessageToSend);
                    await BufferWriter.StoreAsync();
                    BufferWriter.DetachStream();
                    BufferWriter.Dispose();
                    SetOnClientSend(Message);
                }
                else
                {
                    throw new Exception("Cannot send a message, client is disconnected");
                }
            }
            catch (Exception ex)
            {
                SetOnClientException(ex);
                Disconnect();
            }
        }

        public Task<IResponse> SendMessage(IMessage Message, Type ExpectedMessageType)
        {
            Response response = new Response(DateTime.Now, ExpectedMessageType);
            try
            {
                if (IsConnected)
                {
                    if (responseList.Count > 1)
                    {
                        throw new Exception("Cannot have more than one response in a time");
                    }
                    else
                    {
                        responseList.Add(response);
                        SendMessage(Message);
                        response.Handler.WaitOne();
                    }
                }
                else
                {
                    response.IsTimedout = true;
                    throw new Exception("Cannot send a message, client is disconnected");
                }
            }
            catch (Exception ex)
            {
                response.IsTimedout = true;
                SetOnClientException(ex);
                Disconnect();
            }
            return Task.FromResult<IResponse>(response);
        }

        public async void Disconnect()
        {
            try
            {
                if (IsConnected && IsDisconnected == false)
                {
                    IsConnected = false;
                    IsDisconnected = true;
                    responseList.Clear();
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
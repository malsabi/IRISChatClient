using System;
using System.Threading.Tasks;
using IRISChatClient.Configs;
using IRISChatClient.Interfaces;
using IRISChatClient.Networking;

namespace IRISChatClient.Services
{
    public class ClientService : IClientService
    {
        #region "Fields"
        private readonly IRISClient client;
        #endregion

        #region "Properties"
        public bool IsConnected { get { return client.IsConnected; } }

        public bool IsDisconnected { get { return client.IsDisconnected; } }
        #endregion

        #region "Events"
        public event EventHandler<bool> OnStateChanged;

        public event EventHandler<EventArgs> OnAttemptToReconnect;

        public event EventHandler<EventArgs> OnConnected;

        public event EventHandler<EventArgs> OnDisconnected;

        public event EventHandler<IMessage> OnMessageReceived;

        public event EventHandler<IMessage> OnMessageSent;

        public event EventHandler<Exception> OnException;
        #endregion

        #region "Constructors"
        public ClientService()
        {
            client = new IRISClient(Constants.HOST, Constants.PORT, Constants.ATTEMPT_TO_RECONNECT);
        }
        #endregion

        #region "Client"
        public void Connect()
        {
            client.Connect();
        }

        public void Disconnect()
        {
            client.Disconnect();
        }

        public void SendMessage(IMessage Message)
        {
            client.SendMessage(Message);
        }

        public Task<IResponse> SendMessage(IMessage Message, Type ExpectedMessageType)
        {
            return client.SendMessage(Message, ExpectedMessageType);
        }

        public void SetOnStateChanged(bool State)
        {
            OnStateChanged?.Invoke(this, State);
        }
        public void SetOnAttemptToReconnect()
        {
            OnAttemptToReconnect?.Invoke(this, EventArgs.Empty);
        }

        public void SetOnConnected()
        {
            OnConnected?.Invoke(this, EventArgs.Empty);
        }

        public void SetOnDisconnected()
        {
            OnDisconnected?.Invoke(this, EventArgs.Empty);
        }

        public void SetOnMessageReceived(IMessage Message)
        {
            OnMessageReceived?.Invoke(this, Message);
        }

        public void SetOnMessageSent(IMessage Message)
        {
            OnMessageSent?.Invoke(this, Message);
        }

        public void SetOnException(Exception ex)
        {
            OnException?.Invoke(this, ex);
        }

        public void RegisterEvents()
        {
            client.OnClientStateChanged += SetOnStateChanged;
            client.OnClientAttemptToReconnect += SetOnAttemptToReconnect;
            client.OnClientConnected += SetOnConnected;
            client.OnClientDisconnect += SetOnDisconnected;
            client.OnClientReceive += SetOnMessageReceived;
            client.OnClientSend += SetOnMessageSent;
            client.OnClientException += SetOnException;
        }

        public void UnregisterEvents()
        {
            client.OnClientStateChanged -= SetOnStateChanged;
            client.OnClientAttemptToReconnect -= SetOnAttemptToReconnect;
            client.OnClientConnected -= SetOnConnected;
            client.OnClientDisconnect -= SetOnDisconnected;
            client.OnClientReceive -= SetOnMessageReceived;
            client.OnClientSend -= SetOnMessageSent;
            client.OnClientException -= SetOnException;
        }
        #endregion
    }
}
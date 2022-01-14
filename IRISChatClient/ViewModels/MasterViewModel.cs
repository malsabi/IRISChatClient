using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using IRISChatClient.Enums;
using IRISChatClient.Models;
using IRISChatClient.Configs;
using IRISChatClient.Messages;
using IRISChatClient.Interfaces;

namespace IRISChatClient.ViewModels
{
    public class MasterViewModel : ObservableRecipient
    {
        #region "Properties"
        /// <summary>
        /// Represents a <see cref="System.Collections.ObjectModel.ObservableCollection{T}"/> for storing the scenes.
        /// </summary>
        public ScenariosModel Scenarios { get; private set; }

        /// <summary>
        /// Used for handling the state of the client and notify other view models of the state.
        /// </summary>
        public IClientService Client { get; private set; }
        #endregion

        #region "Events / Handlers"
        public delegate void OnNotificationEvent(string Message, NotificationType Type);
        public event OnNotificationEvent OnNotification;
        private void SetOnNotification(string Message, NotificationType Type)
        {
            OnNotification?.Invoke(Message, Type);
        }
        #endregion

        #region "Constructors"
        public MasterViewModel()
        {
            Initialize();
        }
        #endregion

        #region "Initialization"
        private void Initialize()
        {
            Scenarios = new ScenariosModel();
            Client = App.GetService<IClientService>();
            IsActive = true;
        }
        #endregion

        #region "Messenger"
        protected override void OnActivated()
        {
            RegisterEvents();
            Messenger.Register<MasterViewModel, NotificationMessage>(this, (r, m) => { SetOnNotification(m.Message, m.Type); });
        }
        protected override void OnDeactivated()
        {
            UnregisterEvents();
            Messenger.Unregister<MasterViewModel>(this);
        }
        #endregion

        #region "Client"
        private void OnAttemptToReconnectHandler(object sender, System.EventArgs e)
        {
            SetOnNotification(Constants.ATTEMPT_TO_RECONNECT_MESSAGE, NotificationType.WarningMessage);
        }

        private void OnConnectedHadler(object sender, System.EventArgs e)
        {
            SetOnNotification(Constants.CONNECTED_MESSAGE, NotificationType.SuccessMessage);
        }

        private void OnStateChangedHandler(object sender, bool State)
        {
            Messenger.Send(new ClientStateMessage(State));
        }

        private void RegisterEvents()
        {
            Client.OnAttemptToReconnect += OnAttemptToReconnectHandler;
            Client.OnConnected += OnConnectedHadler;
            Client.OnStateChanged += OnStateChangedHandler;
        }

        private void UnregisterEvents()
        {
            Client.OnAttemptToReconnect -= OnAttemptToReconnectHandler;
            Client.OnConnected -= OnConnectedHadler;
            Client.OnStateChanged -= OnStateChangedHandler;
        }
        #endregion
    }
}
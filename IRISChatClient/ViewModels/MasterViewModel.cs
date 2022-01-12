using IRISChatClient.Enums;
using IRISChatClient.Messages;
using IRISChatClient.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace IRISChatClient.ViewModels
{
    public class MasterViewModel : ObservableRecipient
    {
        #region "Fields"
        private ScenariosModel scenarios;
        #endregion

        #region "Properties"
        public ScenariosModel Scenarios
        {
            get
            {
                return scenarios;
            }
        }
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

        #region "Overrided Methods"
        protected override void OnActivated()
        {
            Messenger.Register<MasterViewModel, NotificationMessage>(this, (r, m) => HandleNotification(m));
        }
        #endregion

        #region "Private Methods"
        private void Initialize()
        {
            IsActive = true;
            scenarios = new ScenariosModel();
            scenarios.AddSignedOutItems();
        }
        private void HandleNotification(NotificationMessage Message)
        {
            SetOnNotification(Message.Message, Message.Type);
        }
        #endregion
    }
}
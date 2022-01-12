using IRISChatClient.Interfaces;
using IRISChatClient.Models;
using IRISChatClient.Networking.Messages;
using IRISChatClient.Validations;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace IRISChatClient.ViewModels
{
    public class RecoverViewModel : ObservableObject, IMessageCommand
    {
        #region "Fields"
        private string recoverUsername;
        #endregion

        #region "Bindable Properties"
        public string RecoverUsername
        {
            get
            {
                return recoverUsername;
            }
            set
            {
                SetProperty(ref recoverUsername, value);
            }
        }
        #endregion

        #region "Events / Handlers"
        public delegate void OnRecoverPasswordResultEvent(string Message, bool IsOperationSuccess);
        public event OnRecoverPasswordResultEvent OnRecoverPasswordResult;
        private void SetOnRecoverPasswordResult(string Message, bool IsOperationSuccess)
        {
            OnRecoverPasswordResult?.Invoke(Message, IsOperationSuccess);
        }

        public delegate void OnBackToLoginButtonClickedEvent();
        public event OnBackToLoginButtonClickedEvent OnBackToLoginButtonClicked;
        private void SetOnBackToLoginButtonClicked()
        {
            OnBackToLoginButtonClicked?.Invoke();
        }
        #endregion

        #region "Constructors"
        public RecoverViewModel()
        {
            RecoverUsername = "";
        }
        #endregion

        #region "Interface Methods"
        public bool CanExecute(IMessage Message)
        {
            if (Message.GetType().Equals(typeof(RecoverUserPasswordResultMessage)))
            {
                return true;
            }
            return false;
        }
        public bool CanExecuteFrom(object Sender)
        {
            return true;
        }
        public void Execute(object Sender, IMessage Message)
        {
            RecoverUserPasswordResultMessage RecoverUserPasswordResult = (RecoverUserPasswordResultMessage)Message;
            SetOnRecoverPasswordResult(RecoverUserPasswordResult.ResultMessage, RecoverUserPasswordResult.IsResultSuccess);
        }
        #endregion

        #region "Public Methods"
        public void RecoverPasswordButtonClick()
        {
            RecoverModel recoverModel = new RecoverModel(RecoverUsername);
            ValidationResult validationResult = RecoverValidator.Invalidate(recoverModel);
            if (validationResult.IsOperationSuccess)
            {
                RecoverUserPasswordMessage RecoverUserPassword = new RecoverUserPasswordMessage(RecoverUsername);
                App.GetClientInstance.SendMessage(new MessageWrapper(RecoverUsername.GetType().Name, RecoverUserPassword));
            }
            else
            {
                SetOnRecoverPasswordResult(validationResult.Message, false);
            }
        }
        public void BackToLoginButtonClick()
        {
            SetOnBackToLoginButtonClicked();
        }
        #endregion
    }
}
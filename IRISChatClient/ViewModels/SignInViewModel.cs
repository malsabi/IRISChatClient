using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using IRISChatClient.Enums;
using IRISChatClient.Models;
using IRISChatClient.Configs;
using IRISChatClient.Messages;
using IRISChatClient.Interfaces;

namespace IRISChatClient.ViewModels
{
    public class SignInViewModel : ObservableRecipient
    {
        #region "Properties"
        public IRelayCommand NavigateRecoverCommand { get; private set; }

        public IRelayCommand NavigateRegisterCommand { get; private set; }

        public IAsyncRelayCommand SignInCommand { get; private set; }

        public INavigationService Navigator { get; private set; }

        public IUserSessionService UserSession { get; private set; }

        public SignInModel UserSignIn { get; private set; }
        #endregion

        #region "Constructor"
        public SignInViewModel()
        {
            Initialize();
        }
        #endregion

        #region "Initialization"
        private void Initialize()
        {
            IsActive = true;
            UserSignIn = new SignInModel();
            NavigateRecoverCommand = new RelayCommand(NavigateRecoverPage);
            NavigateRegisterCommand = new RelayCommand(NavigateRegisterPage);
            SignInCommand = new AsyncRelayCommand(SignIn, CanSignIn);
            Navigator = App.GetService<INavigationService>();
            UserSession = App.GetService<IUserSessionService>();
            UserSession?.SetCommand(SignInCommand);
        }
        #endregion

        #region "Messenger"
        protected override void OnActivated()
        {
            Messenger.Register<ClientStateMessage>(this, (r, m) =>
            {
                SignInCommand.NotifyCanExecuteChanged();
            });
        }

        protected override void OnDeactivated()
        {
            Messenger.Unregister<ClientStateMessage>(this);
        }

        private void SetNotification(string Message, NotificationType Type)
        {
            Messenger.Send(new NotificationMessage(Message, Type));
        }
        #endregion

        #region "User Session"
        private async Task SignIn()
        {
            SetNotification(Constants.ATTEMPT_SIGNIN_MESSAGE, NotificationType.InfoMessage);

            ISessionResult SessionResult = await UserSession.SignIn(UserSignIn);
            if (SessionResult.IsSuccess && UserSession.IsSignedIn)
            {
                SetNotification(SessionResult.Message, NotificationType.SuccessMessage);
                NavigateProfilePage();
            }
            else
            {
                SetNotification(SessionResult.Message, NotificationType.ErrorMessage);
            }
        }
        private bool CanSignIn()
        {
            return !UserSession.IsSigningIn && UserSession.Client.IsConnected;
        }
        #endregion

        #region "Navigations"
        private void NavigateProfilePage()
        {
            Navigator.Navigate<ProfileViewModel>();
        }
        private void NavigateRecoverPage()
        {
            Navigator.Navigate<RecoverViewModel>();
        }
        private void NavigateRegisterPage()
        {
            Navigator.Navigate<RegisterViewModel>();
        }
        #endregion
    }
}
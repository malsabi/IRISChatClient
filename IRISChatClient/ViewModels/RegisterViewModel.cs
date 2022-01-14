using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using IRISChatClient.Interfaces;
using IRISChatClient.Messages;
using IRISChatClient.Enums;
using IRISChatClient.Configs;
using IRISChatClient.Models;

namespace IRISChatClient.ViewModels
{
    public class RegisterViewModel : ObservableRecipient
    {
        #region "Properties"
        public IRelayCommand NavigateLoginCommand { get; private set; }

        public IAsyncRelayCommand RegisterUserCommand { get; private set; }

        public INavigationService Navigator { get; private set; }

        public IUserSessionService UserSession { get; private set; }

        public RegisterModel UserRegister { get; private set; }
        #endregion

        #region "Constructors"
        public RegisterViewModel()
        {
            Initialize();
        }
        #endregion

        #region "Initialization"
        private void Initialize()
        {
            IsActive = true;
            UserRegister = new RegisterModel();
            NavigateLoginCommand = new RelayCommand(NavigateLoginPage);
            RegisterUserCommand = new AsyncRelayCommand(RegisterUser, CanRegisterUser);
            Navigator = App.GetService<INavigationService>();
            UserSession = App.GetService<IUserSessionService>();
        }
        #endregion

        #region "Messenger"
        protected override void OnActivated()
        {
            Messenger.Register<ClientStateMessage>(this, (r, m) =>
            {
                RegisterUserCommand.NotifyCanExecuteChanged();
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
        private void NavigateLoginPage()
        {
            if (Navigator.CanGoBack)
            {
                Navigator.GoBack();
            }
            else
            {
                Navigator.Navigate<SignInViewModel>();
            }
        }

        private async Task RegisterUser()
        {
            SetNotification(Constants.ATTEMPT_REGISTER_MESSAGE, NotificationType.InfoMessage);

            ISessionResult SessionResult = await UserSession.Register(UserRegister);
            if (SessionResult.IsSuccess)
            {
                SetNotification(SessionResult.Message, NotificationType.SuccessMessage);
                NavigateLoginPage();
            }
            else
            {
                SetNotification(SessionResult.Message, NotificationType.ErrorMessage);
            }
        }

        private bool CanRegisterUser()
        {
            return !UserSession.IsSignedIn && UserSession.Client.IsConnected;
        }
        #endregion
    }
}
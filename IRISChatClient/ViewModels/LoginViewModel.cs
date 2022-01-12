using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using IRISChatClient.Configs;
using IRISChatClient.Enums;
using IRISChatClient.Interfaces;
using IRISChatClient.Messages;

namespace IRISChatClient.ViewModels
{
    public class LoginViewModel : ObservableRecipient
    {
        #region "Fields"
        private string username;
        private string password;
        private bool isStaySignedIn;
        private bool isSigningIn;
        #endregion

        #region "Bindable Properties"
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                SetProperty(ref username, value);
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value);
            }
        }
        public bool IsStaySignedIn
        {
            get
            {
                return isStaySignedIn;
            }
            set 
            { 
                SetProperty(ref isStaySignedIn, value);
            }
        }
        #endregion

        #region "Commands"
        public IRelayCommand NavigateRecoverCommand { get; private set; }
        public IRelayCommand NavigateRegisterCommand { get; private set; }
        public IAsyncRelayCommand SignInCommand { get; private set; }
        #endregion

        #region "Services"
        public INavigationService Navigator { get; private set; }
        public IUserSessionService UserSession { get; private set; }
        #endregion

        #region "Events / Handlers"
        private void SetNotification(string Message, NotificationType Type)
        {
            Messenger.Send(new NotificationMessage(Message, Type));
        }
        #endregion

        #region "Constructor"
        public LoginViewModel()
        {
            Initialize();
        }
        #endregion

        #region "Initialization"
        private void Initialize()
        {
            NavigateRecoverCommand = new RelayCommand(NavigateRecoverPage, CanNavigate);
            NavigateRegisterCommand = new RelayCommand(NavigateRegisterPage, CanNavigate);
            SignInCommand = new AsyncRelayCommand(SignIn, CanSignIn);

            if (App.Current != null && App.Current.Services != null)
            {
                Navigator = App.Current.Services.GetService<INavigationService>();
                UserSession = App.Current.Services.GetService<IUserSessionService>();
            }
        }
        #endregion

        #region "User Session"
        private async Task SignIn()
        {
            isSigningIn = true;
            SignInCommand.NotifyCanExecuteChanged();
            SetNotification(Constants.ATTEMPT_SIGNIN_MESSAGE, NotificationType.InfoMessage);
            ISessionResult SessionResult = await UserSession.SignIn(UserName, Password, IsStaySignedIn);
            if (SessionResult.IsSuccess && UserSession.IsSignedIn)
            {
                //Successfully signed in.
                SetNotification(SessionResult.Message, NotificationType.SuccessMessage);
            }
            else
            {
                //Failed to sign in.
                SetNotification(SessionResult.Message, NotificationType.ErrorMessage);
            }
            isSigningIn = false;
            SignInCommand.NotifyCanExecuteChanged();
        }
        private bool CanSignIn()
        {
            return isSigningIn == false;
        }
        #endregion

        #region "Navigations"
        private void NavigateRecoverPage()
        {
            Navigator.Navigate<RecoverViewModel>();
        }
        private void NavigateRegisterPage()
        {
            Navigator.Navigate<RegisterViewModel>();
        }
        private bool CanNavigate()
        {
            return true;
        }
        #endregion
    }
}
using System.ComponentModel;
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
    public class ProfileViewModel : ObservableRecipient
    {
        #region "Properties"
        public IAsyncRelayCommand SaveChangesCommand { get; private set; }

        public IAsyncRelayCommand SignOutCommand { get; private set; }

        public IAsyncRelayCommand DeleteAccountCommand { get; private set; }

        public INavigationService Navigator { get; private set; }

        public IUserSessionService UserSession { get; private set; }

        public ProfileModel UserProfile { get; private set; }
        #endregion

        #region "PropertyChanged Handler"
        private void OnPropertyChangingHandler(object sender, PropertyChangedEventArgs e)
        {
            UserProfile.IsUpdated = UserProfile.IsEqual(UserSession.UserProfile);
            SaveChangesCommand.NotifyCanExecuteChanged();
        }
        #endregion

        #region "Constructors"
        public ProfileViewModel()
        {
            Initialize();
        }
        #endregion

        #region Initialization"
        private void Initialize()
        {
            IsActive = true;
            SaveChangesCommand = new AsyncRelayCommand(SaveChanges, CanSaveChanges);
            SignOutCommand = new AsyncRelayCommand(SignOut, CanSignOutOrDeleteAccount);
            DeleteAccountCommand = new AsyncRelayCommand(DeleteAccount, CanSignOutOrDeleteAccount);
            Navigator = App.GetService<INavigationService>();
            UserSession = App.GetService<IUserSessionService>();
            UserProfile = new ProfileModel(UserSession?.UserProfile);
            UserProfile.PropertyChanged += OnPropertyChangingHandler;
        }
        #endregion

        #region "Messenger"
        protected override void OnActivated()
        {
            Messenger.Register<ClientStateMessage>(this, (r, m) =>
            {
                SignOutCommand.NotifyCanExecuteChanged();
                SaveChangesCommand.NotifyCanExecuteChanged();
                DeleteAccountCommand.NotifyCanExecuteChanged();
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
        private async Task SaveChanges()
        {
            SetNotification(Constants.ATTEMPT_UPDATE_MESSAGE, NotificationType.InfoMessage);
            
            ISessionResult SessionResult = await UserSession.UpdateProfile(UserSession.UserProfile.Username, UserProfile);

            if (SessionResult.IsSuccess && UserSession.IsSignedIn)
            {
                SetNotification(SessionResult.Message, NotificationType.SuccessMessage);
                UserProfile.IsUpdated = true;
                SaveChangesCommand.NotifyCanExecuteChanged();
            }
            else
            {
                SetNotification(SessionResult.Message, NotificationType.ErrorMessage);
            }
        }

        private bool CanSaveChanges()
        {
            return UserProfile.IsUpdated == false && UserSession.Client.IsConnected;
        }

        private async Task SignOut()
        {
            SetNotification(Constants.ATTEMPT_SIGNOUT_MESSAGE, NotificationType.InfoMessage);

            ISessionResult SessionResult = await UserSession.SignOut();

            if (SessionResult.IsSuccess)
            {
                SetNotification(SessionResult.Message, NotificationType.SuccessMessage);
                Navigator.Navigate<SignInViewModel>();
            }
            else
            {
                SetNotification(SessionResult.Message, NotificationType.ErrorMessage);
            }
        }

        private async Task DeleteAccount()
        {
            SetNotification(Constants.ATTEMPT_DELETE_MESSAGE, NotificationType.InfoMessage);

            ISessionResult SessionResult = await UserSession.DeleteAccount();

            if (SessionResult.IsSuccess)
            {
                SetNotification(SessionResult.Message, NotificationType.SuccessMessage);
                Navigator.Navigate<SignInViewModel>();
            }
            else
            {
                SetNotification(SessionResult.Message, NotificationType.ErrorMessage);
            }
        }

        private bool CanSignOutOrDeleteAccount()
        {
            return UserSession.IsSignedIn && UserSession.Client.IsConnected;
        }
        #endregion
    }
}
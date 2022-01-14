using IRISChatClient.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace IRISChatClient.ViewModels
{
    public class RecoverViewModel : ObservableObject
    {
        #region "Fields"
        private string recoverUsername;
        #endregion

        #region "Properties"
        public IRelayCommand NavigateLoginCommand { get; private set; }

        public IAsyncRelayCommand RecoverUserCommand { get; private set; }

        public INavigationService Navigator { get; private set; }

        public IUserSessionService UserSession { get; private set; }

        public string RecoverUsername { get { return recoverUsername; } set { SetProperty(ref recoverUsername, value); } }
        #endregion

        #region "Constructors"
        public RecoverViewModel()
        {
            Initialize();
        }
        #endregion

        #region "Initialization"
        private void Initialize()
        {
            NavigateLoginCommand = new RelayCommand(NavigateLoginPage);
            RecoverUserCommand = new AsyncRelayCommand(RecoverUser, CanRecoverUser);

            if (App.Current != null && App.Current.Services != null)
            {
                Navigator = App.Current.Services.GetService<INavigationService>();
                UserSession = App.Current.Services.GetService<IUserSessionService>();
            }
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

        private async Task RecoverUser()
        {
        }

        private bool CanRecoverUser()
        {
            return true;
        }
        #endregion
    }
}
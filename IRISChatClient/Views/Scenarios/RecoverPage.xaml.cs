using IRISChatClient.Enums;
using IRISChatClient.Networking.MessageManagement;
using Windows.UI.Xaml.Controls;

namespace IRISChatClient.Views.Scenarios
{
    public sealed partial class RecoverPage : Page
    {
        #region "Field"
        //private MasterPage RootPage;
        #endregion

        #region "Constructors"
        public RecoverPage()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region "Handlers"
        private void RecoverPageUnloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //HandleMessage.UnRegister(recoverViewModel);
        }
        private void SetOnRecoverPasswordResult(string Message, bool IsOperationSuccess)
        {
            //if (IsOperationSuccess)
            //{
            //    RootPage.NotifyUser(Message, NotificationType.SuccessMessage);
            //}
            //else
            //{
            //    RootPage.NotifyUser(Message, NotificationType.WarningMessage);
            //}
        }
        private void SetOnBackToLoginButtonClicked()
        {
            //RootPage.NotifyUser(string.Empty, NotificationType.InfoMessage);
            //Frame.Navigate(typeof(LoginPage));
        }
        #endregion

        #region "Private Methods"
        private void Initialize()
        {
            //Unloaded += RecoverPageUnloaded;
            //recoverViewModel.OnRecoverPasswordResult += SetOnRecoverPasswordResult;
            //recoverViewModel.OnBackToLoginButtonClicked += SetOnBackToLoginButtonClicked;
            //HandleMessage.Register(recoverViewModel);
        }
        #endregion

    }
}

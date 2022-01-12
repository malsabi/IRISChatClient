using IRISChatClient.Configs;
using IRISChatClient.Enums;
using IRISChatClient.Helpers;
using IRISChatClient.Networking.MessageManagement;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace IRISChatClient.Views.Scenarios
{
    /// <summary>
    /// This page represetns the back code of the register page.
    /// NOTE:: All of the codes here is related to the "VIEW",
    /// and it's not related to the logical code that is written
    /// in the RegisterViewModel.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        #region "Fields"
        private MasterPage RootPage;
        #endregion

        #region "Constructors"
        public RegisterPage()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region "Handlers"
        private void RegisterPageUnloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //HandleMessage.UnRegister(registerViewModel);
        }
        private async void SetOnRegisterUserHandler(string ResultMessage, bool IsResultSuccess)
        {
            //if (IsResultSuccess)
            //{
            //    RootPage.NotifyUser(ResultMessage, NotificationType.SuccessMessage);
            //    if (RootPage.Dispatcher.HasThreadAccess)
            //    {
            //        Frame.Navigate(typeof(LoginPage));
            //    }
            //    else
            //    {
            //        await RootPage.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { Frame.Navigate(typeof(LoginPage)); });
            //    }
            //}
            //else
            //{
            //    RootPage.NotifyUser(ResultMessage, NotificationType.WarningMessage);
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
            //Unloaded += RegisterPageUnloaded;
            //registerViewModel.OnRegisterUserResult += SetOnRegisterUserHandler;
            //registerViewModel.OnBackToLoginButtonClicked += SetOnBackToLoginButtonClicked;
            //HandleMessage.Register(registerViewModel);
        }
        #endregion
    }
}

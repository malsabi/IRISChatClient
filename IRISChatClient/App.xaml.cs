using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Activation;
using Microsoft.Extensions.DependencyInjection;
using IRISChatClient.Interfaces;
using IRISChatClient.Services;
using IRISChatClient.Views;

namespace IRISChatClient
{
    sealed partial class App : Application
    {
        #region "Fields"
        //private bool IsDialogShowing = false;
        //private bool IsAppLaunched = false;
        #endregion

        #region "Properties"
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; }
        #endregion

        #region "Events / Handlers"
        //private void SetOnClientConnected(IRISClient Client)
        //{
        //    Log(string.Format("CLIENT[{0}] Connected Successfully", Client.EndPoint));
        //}
        //private void SetOnClientSend(IRISClient Client, IMessage Message)
        //{
        //    Log(string.Format("CLIENT[{0}] Sent: {1}", Client.EndPoint, Message.GetType().Name));
        //}
        //private void SetOnClientReceive(IRISClient Client, IMessage Message)
        //{
        //    Log(string.Format("CLIENT[{0}] Received: {1}", Client.EndPoint, Message.GetType().Name));
        //    HandleMessage.Process(Client, Message);
        //}
        //private void SetOnClientException(IRISClient Client, Exception ex)
        //{
        //    Log(string.Format("CLIENT[{0}] Exception occured: {1}", Client.EndPoint, ex.Message));
        //}
        //private void SetOnClientDisconnect()
        //{
        //    Log("Cient Disconnected Successfully");
        //}
        #endregion

        #region "Constructors"
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Current = this;
            Services = ConfigureServices();
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region "Overrided Methods"

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (!(Window.Current.Content is Frame rootFrame))
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }
            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MasterPage), e.Arguments);
                }
                Window.Current.Activate();
            }
            //IsAppLaunched = true;
        }
        #endregion

        #region "Private Methods"
        private void Initialize()
        {
            //client = new IRISClient("127.0.0.1", "1669", true);
            //client.OnClientConnected += SetOnClientConnected;
            //client.OnClientSend += SetOnClientSend;
            //client.OnClientReceive += SetOnClientReceive;
            //client.OnClientException += SetOnClientException;
            //client.OnClientDisconnect += SetOnClientDisconnect;
            //client.Connect();
            //new Thread(new ThreadStart(NetworkCheckerBackground)) { IsBackground = true }.Start();
        }

        //private async void NetworkCheckerBackground()
        //{
        //    while (true)
        //    {
        //        if (client.IsConnected == false && IsDialogShowing == false)
        //        {
        //            if (IsAppLaunched)
        //            {
        //                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
        //                {
        //                    ContentDialog NetworkDialog = new ContentDialog()
        //                    {
        //                        Title = "Network Connection Failure",
        //                        Content = "Failed to connect to the server. Attempting to reconnect to the server.",
        //                        PrimaryButtonText = "OK",
        //                    };
        //                    IsDialogShowing = true;
        //                    ContentDialogResult Result = await NetworkDialog.ShowAsync();
        //                    if (Result == ContentDialogResult.Primary)
        //                    {
        //                        IsDialogShowing = false;
        //                    }
        //                });
        //            }
        //        }
        //        Thread.Sleep(50);
        //    }
        //}

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IUserSessionService, UserSessionService>();
            services.AddSingleton<INavigationService, NavigationService>();
            return services.BuildServiceProvider();
        }
        #endregion

        #region "Logger"
        public void Log(string Message)
        {
            Debug.WriteLine(Message);
        }
        #endregion
    }
}
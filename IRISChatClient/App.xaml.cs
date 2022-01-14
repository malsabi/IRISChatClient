using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Activation;
using Microsoft.Extensions.DependencyInjection;
using IRISChatClient.Views;
using IRISChatClient.Services;
using IRISChatClient.ViewModels;
using IRISChatClient.Interfaces;
using IRISChatClient.Views.Scenarios;

namespace IRISChatClient
{
    sealed partial class App : Application 
    {
        #region "Properties"
        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider Services { get; private set; }
        #endregion

        #region "Constructors"
        public App()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region "Application Lifecycle"
        /// <summary>
        /// Invoked when the application is launched normally by the end user.
        /// Other entry points will be used such as when the application is 
        /// launched to open a specific file.
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
        }
        #endregion

        #region "Initialization"
        /// <summary>
        /// Initializes the services and sets the current to this instance.
        /// </summary>
        private void Initialize()
        {
            Current = this;
            Services = ConfigureServices();
            InitializeServices();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IUserSessionService, UserSessionService>();
            services.AddSingleton<INavigationService, NavigationService>();
            return services.BuildServiceProvider();
        }

        /// <summary>
        /// This method is used to initialize all of the services after they are configured directly.
        /// </summary>
        private void InitializeServices()
        {
            INavigationService Navigator = Services.GetService<INavigationService>();
            Navigator.RegisterView(typeof(MasterViewModel), typeof(MasterPage));
            Navigator.RegisterView(typeof(SignInViewModel), typeof(SignInPage));
            Navigator.RegisterView(typeof(ProfileViewModel), typeof(ProfilePage));
            Navigator.RegisterView(typeof(RegisterViewModel), typeof(RegisterPage));
            Navigator.RegisterView(typeof(RecoverViewModel), typeof(RecoverPage));

            IClientService Client = Services.GetService<IClientService>();
            Client.RegisterEvents();
            Client.Connect();
        }
        #endregion

        #region "Service"
        /// <typeparam name="T">Represents the service interface.</typeparam>
        /// <returns>Returns a service <see cref="T"/> from the service provider.</returns>
        public static T GetService<T>()
        {
            if (Current == null)
            {
                return default;
            }
            else
            {
                return Current.Services.GetService<T>();
            }
        }
        #endregion

        #region "Logger"
        /// <summary>
        /// Represents a global logger that can be accessed with the <see cref="Current"/> instance.
        /// </summary>
        /// <param name="Message"></param>
        public void Log(string Message)
        {
            Debug.WriteLine(Message);
        }
        #endregion
    }
}
using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Microsoft.Extensions.DependencyInjection;
using IRISChatClient.Configs;
using IRISChatClient.Enums;
using IRISChatClient.Models;
using IRISChatClient.ViewModels;
using IRISChatClient.Views.Scenarios;
using IRISChatClient.Interfaces;

namespace IRISChatClient.Views
{
    public sealed partial class MasterPage : Page
    {
        #region "Fields"
        private DispatcherTimer DispatcherTimer;
        #endregion

        #region "Properties"
        /// <summary>
        /// Returns the instance of the masterViewMode.
        /// </summary>
        public MasterViewModel GetMasterViewModel
        {
            get
            {
                return masterViewModel;
            }
        }
        #endregion

        #region "Constructors"
        public MasterPage()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region "Handlers"
        private void DispatcherTimerTick(object sender, object e)
        {
            NotificationBorder.Visibility = Visibility.Collapsed;
            NotificationPanel.Visibility = Visibility.Collapsed;
            DispatcherTimer.Stop();
        }
        private void ToggleButtonClick(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }
        private void ScenarioControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox scenarioListBox = sender as ListBox;
            if (scenarioListBox.SelectedItem is ScenarioModel s)
            {
                ScenarioFrame.Navigate(s.ClassType);
            }
        }
        private void MasterViewModelOnNotification(string Message, NotificationType Type)
        {
            NotifyUser(Message, Type);
        }
        #endregion

        #region "Private Methods"
        private void Initialize()
        {
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += new EventHandler<object>(DispatcherTimerTick);
            DispatcherTimer.Interval = new TimeSpan(0, 0, Constants.MAXIMUM_SHOW_SECONDS);
            masterViewModel.OnNotification += MasterViewModelOnNotification;
            ConfigureNavigationService();
        }

        private void ConfigureNavigationService()
        {
            INavigationService navigationService = App.Current.Services.GetService<INavigationService>();
            navigationService.RegisterView(typeof(MasterViewModel),   typeof(MasterPage));
            navigationService.RegisterView(typeof(LoginViewModel),    typeof(LoginPage));
            navigationService.RegisterView(typeof(ProfileViewModel),  typeof(ProfilePage));
            navigationService.RegisterView(typeof(RegisterViewModel), typeof(RegisterPage));
            navigationService.RegisterView(typeof(RecoverViewModel),  typeof(RecoverPage));
            navigationService.SetCurrentFrame(ScenarioFrame);
        }
        private async void NotifyUser(string Message, NotificationType Type)
        {
            if (Dispatcher.HasThreadAccess == false)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => NotifyUser(Message, Type));
            }
            else
            {
                switch (Type)
                {
                    case NotificationType.InfoMessage:
                        NotificationBorder.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 40, 168, 150));
                        NotificationTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        break;
                    case NotificationType.SuccessMessage:
                        NotificationBorder.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 119, 163, 64));
                        NotificationTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        break;
                    case NotificationType.WarningMessage:
                        NotificationBorder.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 228, 92, 36));
                        NotificationTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        break;
                    case NotificationType.ErrorMessage:
                        NotificationBorder.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 184, 76, 75));
                        NotificationTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        break;
                }
                NotificationTextBlock.Text = Message;
                NotificationBorder.Visibility = (NotificationTextBlock.Text != string.Empty) ? Visibility.Visible : Visibility.Collapsed;
                if (NotificationTextBlock.Text != string.Empty)
                {
                    NotificationBorder.Visibility = Visibility.Visible;
                    NotificationPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    NotificationBorder.Visibility = Visibility.Collapsed;
                    NotificationPanel.Visibility = Visibility.Collapsed;
                }
                if (DispatcherTimer.IsEnabled)
                {
                    DispatcherTimer.Stop();
                }
                DispatcherTimer.Start();
            }
        }
        #endregion
    }
}
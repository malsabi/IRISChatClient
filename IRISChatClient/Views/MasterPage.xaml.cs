using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using IRISChatClient.Configs;
using IRISChatClient.Enums;
using IRISChatClient.Models;
using IRISChatClient.Interfaces;

namespace IRISChatClient.Views
{
    public sealed partial class MasterPage : Page
    {
        #region "Fields"
        private DispatcherTimer notificationTimer;
        #endregion

        #region "Constructors"
        public MasterPage()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region "Handlers"
        private void ScenarioControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox scenarioListBox = sender as ListBox;
            if (scenarioListBox.SelectedItem is ScenarioModel s)
            {
                ScenarioFrame.Navigate(s.ClassType);
            }
        }
        private void ToggleButtonClick(object sender, RoutedEventArgs e)
        {
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }
        private void NotificationTimerHandler(object sender, object e)
        {
            NotificationBorder.Visibility = Visibility.Collapsed;
            NotificationPanel.Visibility = Visibility.Collapsed;
            notificationTimer.Stop();
        }
        private void OnNotificationHandler(string Message, NotificationType Type)
        {
            NotifyUser(Message, Type);
        }
        #endregion

        #region "Initialization"
        private void Initialize()
        {
            App.GetService<INavigationService>().SetCurrentFrame(ScenarioFrame);
            notificationTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, Constants.MAXIMUM_SHOW_SECONDS)
            };
            notificationTimer.Tick += NotificationTimerHandler;
            masterViewModel.OnNotification += OnNotificationHandler;
        }
        #endregion

        #region "Notification"
        private async void NotifyUser(string Message, NotificationType Type)
        {
            if (Dispatcher.HasThreadAccess == false)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => NotifyUser(Message, Type));
            }
            else
            {
                if (notificationTimer.IsEnabled)
                {
                    notificationTimer.Stop();
                }
                notificationTimer.Start();
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
            }
        }
        #endregion
    }
}
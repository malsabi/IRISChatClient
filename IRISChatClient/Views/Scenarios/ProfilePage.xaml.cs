using IRISChatClient.Enums;
using IRISChatClient.Models;
using IRISChatClient.Networking.MessageManagement;
using System;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IRISChatClient.Views.Scenarios
{
    public sealed partial class ProfilePage : Page
    {
        #region "Fields"
        //private MasterPage RootPage;
        #endregion

        #region "Properties"
        #endregion

        #region "Events / Handlers"
        private async void SetOnSignOutUserResult(string ResultMessage, bool IsResultSuccess)
        {
            //if (IsResultSuccess)
            //{
            //    RootPage.NotifyUser(ResultMessage, NotificationType.SuccessMessage);
            //    if (RootPage.Dispatcher.HasThreadAccess)
            //    {
            //        RootPage.GetMasterViewModel.Scenarios.AddSignedOutItems();
            //    }
            //    else
            //    {
            //        await RootPage.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => 
            //        {
            //            RootPage.GetMasterViewModel.Scenarios.AddSignedOutItems();
            //        });
            //    }
            //}
            //else
            //{
            //    RootPage.NotifyUser(ResultMessage, NotificationType.WarningMessage);
            //}
        }
        private void SetOnUpdateUserProfileResult(ProfileModel UpdatedUserProfile, string ResultMessage, bool IsResultSuccess)
        {
            //if (IsResultSuccess)
            //{
            //    RootPage.NotifyUser(ResultMessage, NotificationType.SuccessMessage);
            //    profileViewModel.UserProfile = UpdatedUserProfile;
            //}
            //else
            //{
            //    RootPage.NotifyUser(ResultMessage, NotificationType.WarningMessage);
            //}
        }
        private async void SetOnDeleteUserAccountResult(string ResultMessage, bool IsResultSuccess)
        {
            //if (IsResultSuccess)
            //{
            //    RootPage.NotifyUser(ResultMessage, NotificationType.SuccessMessage);
            //    if (RootPage.Dispatcher.HasThreadAccess)
            //    {
            //        RootPage.GetMasterViewModel.Scenarios.AddSignedOutItems();
            //    }
            //    else
            //    {
            //        await RootPage.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            //        {
            //            RootPage.GetMasterViewModel.Scenarios.AddSignedOutItems();
            //        });
            //    }
            //}
            //else
            //{
            //    RootPage.NotifyUser(ResultMessage, NotificationType.WarningMessage);
            //}
        }
        #endregion

        #region "Constructors"
        public ProfilePage()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region "Private Methods"
        private void Initialize()
        {
            //Unloaded += ProfilePageUnloaded;
            //RootPage = MasterPage.Current;
            //profileViewModel.LoadProfile(RootPage.UserProfile);
        }
        #endregion
    }
}

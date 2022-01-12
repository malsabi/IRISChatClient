using System;
using IRISChatClient.Interfaces;
using IRISChatClient.Models;
using IRISChatClient.Networking;
using IRISChatClient.Networking.Messages;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace IRISChatClient.ViewModels
{
    public class ProfileViewModel : ObservableObject, IMessageCommand
    {
        #region "Fields"
        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private DateTimeOffset dateOfBirth;
        #endregion

        #region "Properties"
        public ProfileModel UserProfile { get; set; }
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                SetProperty(ref firstName, value);
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                SetProperty(ref lastName, value);
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetProperty(ref email, value);
            }
        }
        public string Username
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
        public DateTimeOffset DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                SetProperty(ref dateOfBirth, value);
            }
        }
        #endregion

        #region "Events / Handlers"
        public delegate void OnSignOutUserResultEvent(string ResultMessage, bool IsResultSuccess);
        public event OnSignOutUserResultEvent OnSignOutUserResult;
        private void SetOnSignOutUserResult(string ResultMessage, bool IsResultSuccess)
        {
            OnSignOutUserResult?.Invoke(ResultMessage, IsResultSuccess);
        }

        public delegate void OnUpdateUserProfileResultEvent(ProfileModel UpdatedUserProfile, string ResultMessage, bool IsResultSuccess);
        public event OnUpdateUserProfileResultEvent OnUpdateUserProfileResult;
        private void SetOnUpdateUserProfileResult(ProfileModel UpdatedUserProfile, string ResultMessage, bool IsResultSuccess)
        {
            OnUpdateUserProfileResult?.Invoke(UpdatedUserProfile, ResultMessage, IsResultSuccess);
        }

        public delegate void OnDeleteUserAccountResultEvent(string ResultMessage, bool IsResultSuccess);
        public event OnDeleteUserAccountResultEvent OnDeleteUserAccountResult;
        private void SetOnDeleteUserAccountResult(string ResultMessage, bool IsResultSuccess)
        {
            OnDeleteUserAccountResult?.Invoke(ResultMessage, IsResultSuccess);
        }

        public delegate void OnSignOutButtonClickedEvent();
        public event OnSignOutButtonClickedEvent OnSignOutButtonClicked;
        private void SetOnSignOutButtonClicked()
        {
            OnSignOutButtonClicked?.Invoke();
        }

        public delegate void OnSaveChangesButtonClickedEvent();
        public event OnSaveChangesButtonClickedEvent OnSaveChangesButtonClicked;
        private void SetOnSaveChangesButtonClicked()
        {
            OnSaveChangesButtonClicked?.Invoke();
        }

        public delegate void OnDeleteAccountButtonClickedEvent();
        public event OnDeleteAccountButtonClickedEvent OnDeleteAccountButtonClicked;
        private void SetOnDeleteAccountButtonClicked()
        {
            OnDeleteAccountButtonClicked?.Invoke();
        }
        #endregion

        #region "Constructors"
        public ProfileViewModel()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Username = "";
            DateOfBirth = DateTime.Now;
        }
        #endregion

        #region "Interface Methods"
        public bool CanExecute(IMessage Message)
        {
            if (Message.GetType().Equals(typeof(SignOutUserResultMessage)))
            {
                return true;
            }
            else if (Message.GetType().Equals(typeof(UpdateUserProfileResultMessage)))
            {
                return true;
            }
            else if (Message.GetType().Equals(typeof(DeleteUserAccountResultMessage)))
            {
                return true;
            }
            return false;
        }
        public bool CanExecuteFrom(object Sender)
        {
            return true;
        }
        public void Execute(object Sender, IMessage Message)
        {
            if (Message.GetType().Equals(typeof(SignOutUserResultMessage)))
            {
                SignOutUserResult((IRISClient)Sender, (SignOutUserResultMessage)Message);
            }
            else if (Message.GetType().Equals(typeof(UpdateUserProfileResultMessage)))
            {
                UpdateUserProfileResult((IRISClient)Sender, (UpdateUserProfileResultMessage)Message);
            }
            else if (Message.GetType().Equals(typeof(DeleteUserAccountResultMessage)))
            {
                DeleteUserAccountResult((IRISClient)Sender, (DeleteUserAccountResultMessage)Message);
            }
        }
        #endregion

        #region "Private Methods"
        private void SignOutUserResult(IRISClient Client, SignOutUserResultMessage Message)
        {
            SetOnSignOutUserResult(Message.ResultMessage, Message.IsResultSuccess);
        }
        private void UpdateUserProfileResult(IRISClient Client, UpdateUserProfileResultMessage Message)
        {
            SetOnUpdateUserProfileResult(Message.UpdatedUserProfile, Message.ResultMessage, Message.IsResultSuccess);
        }
        private void DeleteUserAccountResult(IRISClient Client, DeleteUserAccountResultMessage Message)
        {
            SetOnDeleteUserAccountResult(Message.ResultMessage, Message.IsResultSuccess);
        }
        #endregion

        #region "Public Methods"
        public void LoadProfile(ProfileModel UserProfile)
        {
            this.UserProfile = UserProfile;
            if (UserProfile != null)
            {
                FirstName = UserProfile.FirstName;
                LastName = UserProfile.LastName;
                Email = UserProfile.Email;
                Username = UserProfile.Username;
                DateOfBirth = UserProfile.DateOfBirth;
            }
        }
        public void SignOutButtonClick()
        {
            SignOutUserMessage SignOutUser = new SignOutUserMessage(UserProfile.Username);
            App.GetClientInstance.SendMessage(new MessageWrapper(typeof(SignOutUserMessage).Name, SignOutUser));
            SetOnSignOutButtonClicked();
        }
        public async void SaveChangesButtonClick()
        {

            ProfileModel CurrentProfile = new ProfileModel(FirstName, LastName, Email, Username, DateOfBirth.Date);

            if (CurrentProfile.IsEqual(UserProfile))
            {
                ContentDialog DeleteConfirmationDialog = new ContentDialog
                {
                    Title = "Save Changes",
                    Content = "You didn't make any changes",
                    PrimaryButtonText = "Ok",
                };
                await DeleteConfirmationDialog.ShowAsync();
            }
            else
            {
                UpdateUserProfileMessage UpdateUserProfile = new UpdateUserProfileMessage
                {
                    OldUsername = UserProfile.Username,
                    UpdatedUserProfile = new ProfileModel(FirstName, LastName, Email, Username, DateOfBirth.Date)
                };
                App.GetClientInstance.SendMessage(new MessageWrapper(typeof(UpdateUserProfileMessage).Name, UpdateUserProfile));
            }
            SetOnSaveChangesButtonClicked();
        }
        public async void DeleteAccountButtonClickAsync()
        {
            ContentDialog DeleteConfirmationDialog = new ContentDialog
            {
                Title = "Account Termination",
                Content = "Are you sure you want to delete your account ?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel"
            };
            ContentDialogResult Result = await DeleteConfirmationDialog.ShowAsync();
            if (Result == ContentDialogResult.Primary)
            {
                DeleteUserAccountMessage DeleteUserAccount = new DeleteUserAccountMessage(Username);
                App.GetClientInstance.SendMessage(new MessageWrapper(typeof(DeleteUserAccountMessage).Name, DeleteUserAccount));
            }
            SetOnDeleteAccountButtonClicked();
        }
        #endregion
    }
}
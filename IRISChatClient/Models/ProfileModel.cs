using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
namespace IRISChatClient.Models
{
    public class ProfileModel : ObservableObject
    {
        #region "Fields"
        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private DateTimeOffset dateOfBirth;
        private bool isUpdated;
        #endregion

        #region "Properties"
        public string FirstName { get { return firstName; } set { SetProperty(ref firstName, value); } }
        public string LastName { get { return lastName; } set { SetProperty(ref lastName, value); } }
        public string Email { get { return email; } set { SetProperty(ref email, value); } }
        public string Username { get { return username; } set { SetProperty(ref username, value); } }
        public DateTimeOffset DateOfBirth { get { return dateOfBirth; } set { SetProperty(ref dateOfBirth, value); } }
        public bool IsUpdated
        { 
            get { return isUpdated; }
            set 
            {
                if (isUpdated != value)
                {
                    isUpdated = value;
                }
            }
        }
        #endregion

        #region "Constructors"
        public ProfileModel()
        {
            IsUpdated = true;
        }

        public ProfileModel(string FirstName, string LastName, string Email, string Username, DateTimeOffset DateOfBirth)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Username = Username;
            this.DateOfBirth = DateOfBirth;
            IsUpdated = true;
        }

        public ProfileModel(ProfileModel UserProfile)
        {
            if (UserProfile != null)
            {
                FirstName = UserProfile.FirstName;
                LastName = UserProfile.LastName;
                Email = UserProfile.Email;
                Username = UserProfile.Username;
                DateOfBirth = UserProfile.DateOfBirth;
                IsUpdated = true;
            }
        }
        #endregion
        #region "Public Methods"
        public bool IsEqual(ProfileModel UserProfile)
        {
            return UserProfile.FirstName.Equals(FirstName) && UserProfile.LastName.Equals(LastName) && UserProfile.Email.Equals(Email) && UserProfile.Username.Equals(Username) && UserProfile.DateOfBirth.Equals(DateOfBirth);
        }
        #endregion
    }
}
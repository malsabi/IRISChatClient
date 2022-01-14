using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
namespace IRISChatClient.Models
{
    public class RegisterModel : ObservableObject
    {
        #region "Fields"
        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private string password;
        private string confirmPassword;
        private string gender;
        private bool maleRadioButtonChecked;
        private bool femaleRadioButtonChecked;
        private DateTimeOffset dateOfBirth;
        #endregion

        #region "Properties"
        public string FirstName { get { return firstName; } set { SetProperty(ref firstName, value); } }

        public string LastName { get { return lastName; } set { SetProperty(ref lastName, value); } }

        public string Email { get { return email; } set { SetProperty(ref email, value); } }

        public string Username { get { return username; } set { SetProperty(ref username, value); } }

        public string Password { get { return password; } set { SetProperty(ref password, value); } }

        public string ConfirmPassword { get { return confirmPassword; } set { SetProperty(ref confirmPassword, value); } }

        public string Gender { get { return gender; } set { SetProperty(ref gender, value); } }

        public DateTimeOffset DateOfBirth { get { return dateOfBirth; } set { SetProperty(ref dateOfBirth, value); } }

        public bool MaleRadioButtonChecked
        {
            get { return maleRadioButtonChecked; }
            set
            {
                if (value)
                {
                    Gender = "Male";
                }
                SetProperty(ref maleRadioButtonChecked, value);
            }
        }

        public bool FemaleRadioButtonChecked
        {
            get { return femaleRadioButtonChecked; }
            set
            {
                if (value)
                {
                    Gender = "Female";
                }
                SetProperty(ref femaleRadioButtonChecked, value);
            }
        }
        #endregion

        #region "Constructors"
        public RegisterModel()
        {
        }
        public RegisterModel(string FirstName, string LastName, string Username, string Email, string Password, string ConfirmPassword, DateTimeOffset DateOfBirth, string Gender)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.ConfirmPassword = ConfirmPassword;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
        }
        #endregion
    }
}
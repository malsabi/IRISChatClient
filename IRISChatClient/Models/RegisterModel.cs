using System;
namespace IRISChatClient.Models
{
    public class RegisterModel
    {
        #region "Properties"
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        #endregion

        #region "Constructors"
        public RegisterModel()
        {
            FirstName = "";
            LastName = "";
            Username = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
            DateOfBirth = DateTime.MinValue;
            Gender = "";
        }
        public RegisterModel(string FirstName, string LastName, string Username, string Email, string Password, string ConfirmPassword, DateTime DateOfBirth, string Gender)
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
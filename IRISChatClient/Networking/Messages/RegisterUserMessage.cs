using IRISChatClient.Interfaces;
using IRISChatClient.Models;

namespace IRISChatClient.Networking.Messages
{
    public class RegisterUserMessage : IMessage
    {
        #region "Properties"
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        #endregion

        #region "Constructors"
        public RegisterUserMessage()
        {
        }

        public RegisterUserMessage(string FirstName, string LastName, string Username, string Email, string Password, string DateOfBirth, string Gender)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Username = Username;
            this.Email = Email;
            this.Password = Password;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
        }

        public RegisterUserMessage(RegisterModel UserRegisterInformation)
        {
            FirstName = UserRegisterInformation.FirstName;
            LastName = UserRegisterInformation.LastName;
            Username = UserRegisterInformation.Username;
            Email = UserRegisterInformation.Email;
            Password = UserRegisterInformation.Password;
            DateOfBirth = UserRegisterInformation.DateOfBirth.ToString();
            Gender = UserRegisterInformation.Gender;
        }
        #endregion
    }
}
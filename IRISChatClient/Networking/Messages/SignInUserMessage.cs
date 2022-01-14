using IRISChatClient.Models;
using IRISChatClient.Interfaces;

namespace IRISChatClient.Networking.Messages
{
    public class SignInUserMessage : IMessage
    {
        #region "Properties"
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsStaySignedIn { get; set; }
        #endregion

        #region "Constructors"
        public SignInUserMessage()
        {
        }

        public SignInUserMessage(string Username, string Password, bool IsStaySignedIn)
        {
            this.Username = Username;
            this.Password = Password;
            this.IsStaySignedIn = IsStaySignedIn;
        }

        public SignInUserMessage(SignInModel UserSignInInformation)
        {
            Username = UserSignInInformation.Username;
            Password = UserSignInInformation.Password;
            IsStaySignedIn = UserSignInInformation.IsStaySignedIn;
        }
        #endregion
    }
}
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace IRISChatClient.Models
{
    public class SignInModel : ObservableObject
    {
        #region "Fields"
        private string username;
        private string password;
        private bool isStaySignedIn;
        #endregion

        #region "Properties"
        public string Username { get { return username; } set { SetProperty(ref username, value); } }

        public string Password { get { return password; } set { SetProperty(ref password, value); } }

        public bool IsStaySignedIn { get { return isStaySignedIn; } set { SetProperty(ref isStaySignedIn, value); } }
        #endregion

        #region "Constructors"
        public SignInModel()
        {
        }

        public SignInModel(string Username, string Password, bool IsStaySignedIn)
        {
            this.Username = Username;
            this.Password = Password;
            this.IsStaySignedIn = IsStaySignedIn;
        }
        #endregion
    }
}
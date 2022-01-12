using Microsoft.Toolkit.Mvvm.Input;
using System;

namespace IRISChatClient.Models
{
    public class LoginModel 
    {
        #region "Properties"
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsStaySignedIn { get; set; }
        #endregion

        #region "Constructors"
        public LoginModel()
        {
            Username = "";
            Password = "";
            IsStaySignedIn = false;
        }
        public LoginModel(string Username, string Password, bool IsStaySignedIn)
        {
            this.Username = Username;
            this.Password = Password;
            this.IsStaySignedIn = IsStaySignedIn;
        }

        #endregion
    }
}
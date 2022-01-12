using IRISChatClient.Interfaces;
using IRISChatClient.Models;
using IRISChatClient.Networking.Messages;
using IRISChatClient.Validations;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;

namespace IRISChatClient.ViewModels
{
    public class RegisterViewModel : ObservableObject
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

        #region "Bindable Properties"
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
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value);
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                SetProperty(ref confirmPassword, value);
            }
        }
        public string Gender 
        {
            get
            {
                return gender;
            }
            set
            {
                SetProperty(ref gender, value);
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
        public bool MaleRadioButtonChecked
        {
            get
            {
                return maleRadioButtonChecked;
            }
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
            get
            {
                return femaleRadioButtonChecked;
            }
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

        #region "Events / Handlers"
        //public delegate void OnRegisterUserResultEvent(string Message, bool IsOperationSuccess);
        //public event OnRegisterUserResultEvent OnRegisterUserResult;
        //private void SetOnRegisterUserResult(string Message, bool IsOperationSuccess)
        //{
        //    OnRegisterUserResult?.Invoke(Message, IsOperationSuccess);
        //}

        //public delegate void OnBackToLoginButtonClickedEvent();
        //public event OnBackToLoginButtonClickedEvent OnBackToLoginButtonClicked;
        //private void SetOnBackToLoginButtonClicked()
        //{
        //    OnBackToLoginButtonClicked?.Invoke();
        //}
        #endregion

        #region "Constructors"
        public RegisterViewModel()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Username = "";
            Password = "";
            ConfirmPassword = "";
            Gender = "";
        }
        #endregion

        #region "Interface Methods"
        //public bool CanExecute(IMessage Message)
        //{
        //    if (Message.GetType().Equals(typeof(RegisterUserResultMessage)))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public bool CanExecuteFrom(object Sender)
        //{
        //    return true;
        //}
        //public void Execute(object Sender, IMessage Message)
        //{
        //    RegisterUserResultMessage RegisterUserResult = (RegisterUserResultMessage)Message;
        //    SetOnRegisterUserResult(RegisterUserResult.ResultMessage, RegisterUserResult.IsResultSuccess);
        //}
        #endregion

        #region "Public Methods"
        public void BackToLoginButtonClick()
        {
            //SetOnBackToLoginButtonClicked();
        }
        public void RegisterUserButtonClick()
        {
            //RegisterModel registerModel = new RegisterModel(FirstName, LastName, Username, Email, Password, ConfirmPassword, DateOfBirth.Date, Gender);
            //ValidationResult validationResult = RegisterValidator.Invalidate(registerModel);
            //if (validationResult.IsOperationSuccess)
            //{
            //    RegisterUserMessage RegisterUser = new RegisterUserMessage(FirstName, LastName, Username, Email, Password, DateOfBirth.ToString(), Gender);
            //    App.GetClientInstance.SendMessage(new MessageWrapper(RegisterUser.GetType().Name, RegisterUser));
            //}
            //else
            //{
            //    SetOnRegisterUserResult(validationResult.Message, false);
            //}
        }
        #endregion
    }
}
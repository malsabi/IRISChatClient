using System;
using IRISChatClient.Configs;
using IRISChatClient.Helpers;
using IRISChatClient.Models;

namespace IRISChatClient.Validations
{
    public class LoginValidator
    {
        public static ValidationResult Invalidate(LoginModel loginModel)
        {
            ValidationResult validationResult = new ValidationResult
            {
                TimeStamp = DateTime.Now,
                IsOperationSuccess = true
            };
            if (loginModel == null)
            {
                validationResult.Message = "Login model cannot be null";
                validationResult.IsOperationSuccess = false;
            }
            else
            {
                //Username Validation.
                if (string.IsNullOrEmpty(loginModel.Username))
                {
                    validationResult.Message = "Username cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (loginModel.Username.Length >= Constants.MAXIMUM_USERNAME_LENGTH)
                {
                    validationResult.Message = string.Format("Username cannot exceed more than {0} characters", Constants.MAXIMUM_USERNAME_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsUsernameValid(loginModel.Username))
                {
                    validationResult.Message = "Username cannot contain non-alphabetic characters";
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(loginModel.Password))
                {
                    validationResult.Message = "Password cannot be empty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (loginModel.Password.Length >= Constants.MAXIMUM_PASSWORD_LENGTH)
                {
                    validationResult.Message = string.Format("Password cannot exceed more than {0} characters", Constants.MAXIMUM_PASSWORD_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsPasswordValid(loginModel.Password))
                {
                    validationResult.Message = string.Format("Password cannot contain the following characters: {0} and (space)", string.Join("  ", ValidationHelper.IllegalCharacters));
                    validationResult.IsOperationSuccess = false;
                }
            }
            return validationResult;
        }
    }
}
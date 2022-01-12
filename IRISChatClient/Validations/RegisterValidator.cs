using IRISChatClient.Configs;
using IRISChatClient.Helpers;
using IRISChatClient.Models;
using System;

namespace IRISChatClient.Validations
{
    public class RegisterValidator
    {
        public static ValidationResult Invalidate(RegisterModel registerModel)
        {
            ValidationResult validationResult = new ValidationResult
            {
                Message = "",
                IsOperationSuccess = true
            };

            if (registerModel == null)
            {
                validationResult.Message = "Register model cannot be null";
                validationResult.IsOperationSuccess = false;
            }
            else
            {
                if (string.IsNullOrEmpty(registerModel.FirstName))  //First Name Validation
                {
                    validationResult.Message = "First name cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (registerModel.FirstName.Length >= Constants.MAXIMUM_FIRSTNAME_LENGTH)
                {
                    validationResult.Message = string.Format("First name cannot exceed more than {0} characters", Constants.MAXIMUM_FIRSTNAME_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsNameValid(registerModel.FirstName))
                {
                    validationResult.Message = "First name cannot contain non-alphabetic characters";
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(registerModel.LastName)) //Last Name Validation
                {
                    validationResult.Message = "Last name cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (registerModel.LastName.Length >= Constants.MAXIMUM_LASTNAME_LENGTH)
                {
                    validationResult.Message = string.Format("Last name cannot exceed more than {0} characters", Constants.MAXIMUM_LASTNAME_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsNameValid(registerModel.LastName))
                {
                    validationResult.Message = "Last name cannot contain non-alphabetic characters";
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(registerModel.Email)) //Email Validation
                {
                    validationResult.Message = "Email address cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (registerModel.Email.Length >= Constants.MAXIMUM_EMAIL_LENMGTH)
                {
                    validationResult.Message = string.Format("Email address cannot exceed more than {0} characters", Constants.MAXIMUM_EMAIL_LENMGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(registerModel.Username)) //Username Validation
                {
                    validationResult.Message = "Username cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (registerModel.Username.Length >= Constants.MAXIMUM_USERNAME_LENGTH)
                {
                    validationResult.Message = string.Format("Username cannot exceed more than {0} characters", Constants.MAXIMUM_USERNAME_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsUsernameValid(registerModel.Username))
                {
                    validationResult.Message = "Username cannot contain non-alphabetic characters";
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(registerModel.Password) || string.IsNullOrEmpty(registerModel.ConfirmPassword)) //Password Validation
                {
                    validationResult.Message = "Password cannot be empty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (!registerModel.Password.Equals(registerModel.ConfirmPassword))
                {
                    validationResult.Message = "Password and confirm password are not the same";
                    validationResult.IsOperationSuccess = false;
                }
                else if (registerModel.Password.Length >= Constants.MAXIMUM_PASSWORD_LENGTH)
                {
                    validationResult.Message = string.Format("Password cannot exceed more than {0} characters", Constants.MAXIMUM_PASSWORD_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsPasswordValid(registerModel.Password))
                {
                    validationResult.Message = string.Format("Password cannot contain the following characters: {0} and (space)", string.Join("  ", ValidationHelper.IllegalCharacters));
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(registerModel.Gender) || registerModel.Gender.Equals(Constants.GENDER_NULL_VALUE)) //Gender Validation
                {
                    validationResult.Message = "Gender cannot be empty";
                    validationResult.IsOperationSuccess = false;
                }
            }
            return validationResult;
        }

    }
}
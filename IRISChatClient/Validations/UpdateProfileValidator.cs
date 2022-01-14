using IRISChatClient.Configs;
using IRISChatClient.Helpers;
using IRISChatClient.Models;

namespace IRISChatClient.Validations
{
    public class UpdateProfileValidator
    {
        public static ValidationResult Invalidate(ProfileModel UpdatedUserProfile)
        {
            ValidationResult validationResult = new ValidationResult
            {
                Message = "",
                IsOperationSuccess = true
            };

            if (UpdatedUserProfile == null)
            {
                validationResult.Message = "Register model cannot be null";
                validationResult.IsOperationSuccess = false;
            }
            else
            {
                if (string.IsNullOrEmpty(UpdatedUserProfile.FirstName))  //First Name Validation
                {
                    validationResult.Message = "First name cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (UpdatedUserProfile.FirstName.Length >= Constants.MAXIMUM_FIRSTNAME_LENGTH)
                {
                    validationResult.Message = string.Format("First name cannot exceed more than {0} characters", Constants.MAXIMUM_FIRSTNAME_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsNameValid(UpdatedUserProfile.FirstName))
                {
                    validationResult.Message = "First name cannot contain non-alphabetic characters";
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(UpdatedUserProfile.LastName)) //Last Name Validation
                {
                    validationResult.Message = "Last name cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (UpdatedUserProfile.LastName.Length >= Constants.MAXIMUM_LASTNAME_LENGTH)
                {
                    validationResult.Message = string.Format("Last name cannot exceed more than {0} characters", Constants.MAXIMUM_LASTNAME_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsNameValid(UpdatedUserProfile.LastName))
                {
                    validationResult.Message = "Last name cannot contain non-alphabetic characters";
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(UpdatedUserProfile.Email)) //Email Validation
                {
                    validationResult.Message = "Email address cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (UpdatedUserProfile.Email.Length >= Constants.MAXIMUM_EMAIL_LENMGTH)
                {
                    validationResult.Message = string.Format("Email address cannot exceed more than {0} characters", Constants.MAXIMUM_EMAIL_LENMGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (string.IsNullOrEmpty(UpdatedUserProfile.Username)) //Username Validation
                {
                    validationResult.Message = "Username cannot be emoty";
                    validationResult.IsOperationSuccess = false;
                }
                else if (UpdatedUserProfile.Username.Length >= Constants.MAXIMUM_USERNAME_LENGTH)
                {
                    validationResult.Message = string.Format("Username cannot exceed more than {0} characters", Constants.MAXIMUM_USERNAME_LENGTH);
                    validationResult.IsOperationSuccess = false;
                }
                else if (!ValidationHelper.IsUsernameValid(UpdatedUserProfile.Username))
                {
                    validationResult.Message = "Username cannot contain non-alphabetic characters";
                    validationResult.IsOperationSuccess = false;
                }
            }
            return validationResult;
        }
    }
}
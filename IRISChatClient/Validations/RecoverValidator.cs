using IRISChatClient.Configs;
using IRISChatClient.Helpers;
using IRISChatClient.Models;

namespace IRISChatClient.Validations
{
    public class RecoverValidator
    {
        public static ValidationResult Invalidate(RecoverModel recoverModel)
        {
            ValidationResult validationResult = new ValidationResult
            {
                Message = "",
                IsOperationSuccess = true
            };
            if (recoverModel == null)
            {
                validationResult.Message = "ReocverModel cannot be null";
                validationResult.IsOperationSuccess = false;
            }
            else if (string.IsNullOrEmpty(recoverModel.RecoverUsername))
            {
                validationResult.Message = "Recover username cannot be empty";
                validationResult.IsOperationSuccess = false;
            }
            else if (recoverModel.RecoverUsername.Length >= Constants.MAXIMUM_USERNAME_LENGTH)
            {
                validationResult.Message = string.Format("Recover username cannot exceed more than {0} characters", Constants.MAXIMUM_USERNAME_LENGTH);
                validationResult.IsOperationSuccess = false;
            }
            else if (!ValidationHelper.IsUsernameValid(recoverModel.RecoverUsername))
            {
                validationResult.Message = "Recover username cannot contain non-alphabetic characters";
                validationResult.IsOperationSuccess = false;
            }
            return validationResult;
        }
    }
}
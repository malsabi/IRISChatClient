using System;
namespace IRISChatClient.Validations
{
    public class ValidationResult
    {
        #region "Properties"
        public string Message { get; set; }
        public bool IsOperationSuccess { get; set; }
        public DateTime TimeStamp { get; set; }
        #endregion

        #region "Constructors"
        public ValidationResult()
        {
            Message = "";
            IsOperationSuccess = false;
            TimeStamp = DateTime.MinValue;
        }
        public ValidationResult(string Message, bool IsOperationSuccess, DateTime TimeStamp)
        {
            this.Message = Message;
            this.IsOperationSuccess = IsOperationSuccess;
            this.TimeStamp = TimeStamp;
        }
        #endregion
    }
}
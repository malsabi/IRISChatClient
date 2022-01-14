using IRISChatClient.Interfaces;

namespace IRISChatClient.Services.ServiceResults
{
    public class SessionResult : ISessionResult
    {
        #region "Properties"
        public string Message { get; set; }

        public bool IsSuccess { get; set; }
        #endregion

        #region "Constructors"
        public SessionResult()
        {
            Message = string.Empty;
            IsSuccess = false;
        }
        public SessionResult(string Message, bool IsSuccess)
        {
            this.Message = Message;
            this.IsSuccess = IsSuccess;
        }
        #endregion
    }
}
using IRISChatClient.Interfaces;

namespace IRISChatClient.Networking.Messages
{
    public class RecoverUserPasswordResultMessage : IMessage
    {
        #region "Properties"
        public bool IsResultSuccess { get; set; }
        public string ResultMessage { get; set; }
        #endregion
    }
}
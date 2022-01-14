using IRISChatClient.Enums;
using IRISChatClient.Interfaces;

namespace IRISChatClient.Messages
{
    public sealed class NotificationMessage : INotificationMessage
    {
        #region "Properties"
        public string Message { get; }
        public NotificationType Type { get; }
        #endregion

        #region "Constructors"
        public NotificationMessage() 
        {
            Message = string.Empty;
            Type = NotificationType.None;
        }

        public NotificationMessage(string Message, NotificationType Type)
        {
            this.Message = Message;
            this.Type = Type;
        }
        #endregion
    }
}
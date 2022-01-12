using IRISChatClient.Enums;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace IRISChatClient.Messages
{
    public sealed class NotificationMessage : ValueChangedMessage<string>
    {
        #region "Properties"
        public string Message { get; }
        public NotificationType Type { get; }
        #endregion

        #region "Constructors"
        public NotificationMessage() : base(string.Empty)
        {
            Message = string.Empty;
            Type = NotificationType.None;
        }
        public NotificationMessage(string Message, NotificationType Type) : base(Message)
        {
            this.Message = Message;
            this.Type = Type;
        }
        #endregion
    }
}
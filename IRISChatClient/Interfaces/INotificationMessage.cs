using IRISChatClient.Enums;

namespace IRISChatClient.Interfaces
{
    public interface INotificationMessage
    {
        /// <summary>
        /// Returns a message that is used for notifications.
        /// </summary>
        string Message { get; }
        
        /// <summary>
        /// Returns the notification type.
        /// </summary>
        NotificationType Type { get; }
    }
}
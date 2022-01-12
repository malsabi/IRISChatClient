using IRISChatClient.Services;
namespace IRISChatClient.Interfaces
{
    public interface ISessionResult
    {
        #region "Session Result Properties"
        /// <summary>
        /// Returns the message from the operations that is done in <see cref="UserSessionService"/>.
        /// </summary>
        string Message { get; }
        /// <summary>
        /// Returns true if the operation that is done in <see cref="UserSessionService"/> succeeeded otherwise false.
        /// </summary>
        bool IsSuccess { get; }
        #endregion
    }
}
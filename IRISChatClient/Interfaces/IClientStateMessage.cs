namespace IRISChatClient.Interfaces
{
    public interface IClientStateMessage
    {
        /// <summary>
        /// Returns true if the client is connected to the server otherwise false.
        /// </summary>
        bool IsClientActive { get; }
    }
}
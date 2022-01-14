using System;
using System.Threading;

namespace IRISChatClient.Interfaces
{
    public interface IResponse
    {
        /// <summary>
        /// Represents when was the last time the response was seen.
        /// This is useful for terminating a timed out response.
        /// /// </summary>
        DateTime LastSeen { get; set; }
        /// <summary>
        /// Represents the expected message type response from the server.
        /// </summary>
        Type ExpectedMessageType { get; set; }
        /// <summary>
        /// Represents the response message from the server.
        /// </summary>
        IMessage Result { get; set; }
        /// <summary>
        /// Returns true if the response is timed out otherwise false.
        /// </summary>
        bool IsTimedout { get; set; }
        /// <summary>
        /// Used for waiting the response from the server.
        /// </summary>
        ManualResetEvent Handler { get; set; }
    }
}
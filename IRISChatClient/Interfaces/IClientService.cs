using System;
using System.Threading.Tasks;
using IRISChatClient.Networking;
using IRISChatClient.Networking.Messages;

namespace IRISChatClient.Interfaces
{
    public interface IClientService
    {
        /// <summary>
        /// Exposes an event when the client state such as IsConnected, IsDisconnected, AttemptToReconnect values
        /// are changed.
        /// </summary>
        event EventHandler<bool> OnStateChanged;

        /// <summary>
        /// Exposes an event when a client tries to reconnect to the server.
        /// </summary>
        event EventHandler<EventArgs> OnAttemptToReconnect;

        /// <summary>
        /// Exposes an event when a client is connected.
        /// </summary>
        event EventHandler<EventArgs> OnConnected;

        /// <summary>
        /// Exposes an event when a client is disconnected.
        /// </summary>
        event EventHandler<EventArgs> OnDisconnected;

        /// <summary>
        /// Exposes an event when a client received a message from the server.
        /// </summary>
        event EventHandler<IMessage> OnMessageReceived;

        /// <summary>
        /// Exposes an event when a client send a message to the server.
        /// </summary>
        event EventHandler<IMessage> OnMessageSent;

        /// <summary>
        /// Exposes an event when an exception occurs in the client.
        /// </summary>
        event EventHandler<Exception> OnException;

        /// <summary>
        /// Invokes the <see cref="OnStateChanged"/> event.
        /// </summary>
        void SetOnStateChanged(bool State);

        /// <summary>
        /// Invokes the <see cref="OnAttemptToReconnect"/> event.
        /// </summary>
        void SetOnAttemptToReconnect();

        /// <summary>
        /// Invokes the <see cref="OnConnected"/> event.
        /// </summary>
        void SetOnConnected();

        /// <summary>
        /// Invokes the <see cref="OnDisconnected"/> event.
        /// </summary>
        void SetOnDisconnected();

        /// <summary>
        /// Invokes the <see cref="OnMessageReceived"/> event.
        /// </summary>
        /// <param name="Message">Represents the message received from the server.</param>
        void SetOnMessageReceived(IMessage Message);

        /// <summary>
        /// Invokes the <see cref="OnMessageSent"/> event.
        /// </summary>
        /// <param name="Message">Represents the message sent to the server.</param>
        void SetOnMessageSent(IMessage Message);

        /// <summary>
        /// Invokes the <see cref="OnException"/> event.
        /// </summary>
        /// <param name="ex">Represents the exception occured in the client.</param>
        void SetOnException(Exception ex);

        /// <summary>
        /// Returns true if the client is connected to the server otherwise false.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Returns true if the client is disconnected from the server otherwise false.
        /// </summary>
        bool IsDisconnected { get; }

        /// <summary>
        /// Sends a wrapped message that contains the actual message and the message type.
        /// </summary>
        /// <param name="Message">Represents the wrapped message.</param>
        void SendMessage(IMessage Message);

        /// <summary>
        /// Sends a wrapped message that contains the actual message and the message type,
        /// waits for a response from the server, if the received message was not equal to
        /// the <see cref="ExpectedType"/> then it will return timed out, same goes if the
        /// client did not receive a message for more than 5 seconds.
        /// </summary>
        /// <param name="Message">Represents the <see cref="IMessage"/> interface that all messages implements.</param>
        /// <param name="ExpectedMessageType">Represents the expected message type response from the server.</param>
        /// <returns></returns>
        Task<IResponse> SendMessage(IMessage Message, Type ExpectedMessageType);

        /// <summary>
        /// Connects to the server and will attempt to reconnect if the <see cref="AttemptToReconnect"/>
        /// was set to true otherwise it will just connect one time.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects from the server, but it will attempt to reconnect if the <see cref="AttemptToReconnect"/>
        /// was set to true otherwise it will just disconnect from the server.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Subscribes the events with the client.
        /// </summary>
        void RegisterEvents();

        /// <summary>
        /// Unsubscribes the events from the client.
        /// </summary>
        void UnregisterEvents();
    }
}
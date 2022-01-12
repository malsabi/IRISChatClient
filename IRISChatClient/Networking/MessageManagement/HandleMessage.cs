﻿using IRISChatClient.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IRISChatClient.Networking.MessageManagement
{
    /// <summary>
    /// This class is responsible for handling the message when
    /// it is finished processing, it will handle it to the 
    /// registered command processors that will execute the message.
    /// </summary>
    public static class HandleMessage
    {

        #region "Static Fields"
        /// <summary>
        /// List of registered message commands.
        /// </summary>
        private static readonly List<IMessageCommand> MessageCommands = new List<IMessageCommand>();
        /// <summary>
        /// Used for synchronized access when calling process method from different threads.
        /// </summary>
        private static readonly object SyncLock = new object();
        #endregion


        #region "Public Static Methods"
        /// <summary>
        /// Add a MessageCommand to the list that will be responsible for handling the messages.
        /// </summary>
        /// <param name="MessageCommand"></param>
        public static void Register(IMessageCommand MessageCommand)
        {
            lock (SyncLock)
            {
                Log("HandleMessage: Register Method executed, MessageCommand Name: " + MessageCommand.GetType().Name);
                if (MessageCommands.Contains(MessageCommand) == false)
                {
                    MessageCommands.Add(MessageCommand);
                }
            }
        }
        /// <summary>
        /// Removes a MessageCommand from the list.
        /// </summary>
        /// <param name="MessageCommand"></param>
        public static void UnRegister(IMessageCommand MessageCommand)
        {
            lock (SyncLock)
            {
                Log("HandleMessage: UnRegister Method executed, MessageCommand Name: " + MessageCommand.GetType().Name);
                MessageCommands.Remove(MessageCommand);
            }
        }
        /// <summary>
        /// Forwards the received <see cref="IMessage"/> to the appropriate <see cref="IMessageCommand"/>s to execute it.
        /// </summary>
        /// <param name="sender">The sender of the message.</param>
        /// <param name="msg">The received message.</param>
        public static void Process(object sender, IMessage Message)
        {
            Log("HandleMessage: Process Method executed, Message Name: " + Message.GetType().Name);
            IEnumerable<IMessageCommand> AvailableMessageCommands;
            lock (SyncLock)
            {
                AvailableMessageCommands = MessageCommands.Where(x => x.CanExecute(Message) && x.CanExecuteFrom(sender)).ToList();
            }

            foreach (IMessageCommand Executor in AvailableMessageCommands)
            {
                Executor.Execute(sender, Message);
            }
        }
        #endregion
        #region "Logger"
        private static void Log(string Message)
        {
            Debug.WriteLine(Message);
        }
        #endregion
    }
}
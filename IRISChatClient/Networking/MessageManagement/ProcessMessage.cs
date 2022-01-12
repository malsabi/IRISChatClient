using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using IRISChatClient.Interfaces;
using IRISChatClient.Networking.Encryption;
using IRISChatClient.Networking.Messages;

namespace IRISChatClient.Networking.MessageManagement
{
    /// <summary>
    /// Handles the messages received and deserialize them depending on the target type.
    /// Performance: The class methods are static, therefore we won't allocate memory on each
    /// receive call, since we may have more than 100p/s.
    /// </summary>
    public static class ProcessMessage
    {
        #region "Private Static Methods"
        private static IMessage DeserializeToMessage(string MessageType, object Message)
        {
            if (MessageType.Equals(nameof(RecoverUserPasswordResultMessage)))
            {
                return JsonConvert.DeserializeObject<RecoverUserPasswordResultMessage>(Convert.ToString(Message));
            }
            else if (MessageType.Equals(nameof(RegisterUserResultMessage)))
            {
                return JsonConvert.DeserializeObject<RegisterUserResultMessage>(Convert.ToString(Message));
            }
            else if (MessageType.Equals(nameof(SignInUserResultMessage)))
            {
                return JsonConvert.DeserializeObject<SignInUserResultMessage>(Convert.ToString(Message));
            }
            else if (MessageType.Equals(nameof(SignOutUserResultMessage)))
            {
                return JsonConvert.DeserializeObject<SignOutUserResultMessage>(Convert.ToString(Message));
            }
            else if (MessageType.Equals(nameof(UpdateUserProfileResultMessage)))
            {
                return JsonConvert.DeserializeObject<UpdateUserProfileResultMessage>(Convert.ToString(Message));
            }
            else if (MessageType.Equals(nameof(DeleteUserAccountResultMessage)))
            {
                return JsonConvert.DeserializeObject<DeleteUserAccountResultMessage>(Convert.ToString(Message));
            }
            else
            {
                return null;
            }
        }
        private static IEnumerable<Type> GetRegisteredMessages()
        {
            Type Messagetype = typeof(IMessage);
            IEnumerable<Type> RegisteredTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => Messagetype.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
            return RegisteredTypes;
        }
        #endregion

        #region "Public Static Methods"
        public static IMessage Process(byte[] EncryptedPacket)
        {
            try
            {
                //1. Decrypt the AES from the Encrypted Packet, if an exception occured we will return null.
                string DecryptedJsonString = AES256.BytesToString(AES256.Decrypt(EncryptedPacket));
                //2. Deserialize the DecryptedJsonString into MessageWrapper, if an exception occured we will return null.
                MessageWrapper WrappedMessage = JsonConvert.DeserializeObject<MessageWrapper>(DecryptedJsonString);
                //3. Validate the Message and check if the MessageType are registered.
                bool IsMessageTypeRegistered = false;
                foreach (Type RegisteredType in GetRegisteredMessages())
                {
                    if (RegisteredType.Name.Equals(WrappedMessage.MessageType))
                    {
                        IsMessageTypeRegistered = true;
                        break;
                    }
                }
                //4. If Message type is not found then return null, source can be from an attacker or unknown source.
                if (IsMessageTypeRegistered == false)
                {
                    return null;
                }
                else
                {
                    //5. Dserialize the message by using the MessageType and return the result.
                    IMessage Message = DeserializeToMessage(WrappedMessage.MessageType, WrappedMessage.Message);
                    //6. Return the message to the client.
                    return Message;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
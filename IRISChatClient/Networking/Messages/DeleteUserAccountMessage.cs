using IRISChatClient.Interfaces;

namespace IRISChatClient.Networking.Messages
{
    public class DeleteUserAccountMessage : IMessage
    {
        #region "Properties"
        public string Username { get; set; }
        #endregion

        #region "Constructors"
        public DeleteUserAccountMessage()
        {
            Username = "";
        }
        public DeleteUserAccountMessage(string Username)
        {
            this.Username = Username;
        }
        #endregion
    }
}
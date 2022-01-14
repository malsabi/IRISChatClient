using IRISChatClient.Interfaces;
namespace IRISChatClient.Messages
{
    public sealed class ClientStateMessage : IClientStateMessage
    {
        #region "Properties"
        public bool IsClientActive { get; private set; }
        #endregion

        #region "Constructors"
        public ClientStateMessage()
        {
            IsClientActive = false;
        }

        public ClientStateMessage(bool IsClientActive)
        {
            this.IsClientActive = IsClientActive;
        }
        #endregion
    }
}

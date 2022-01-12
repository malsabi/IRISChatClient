namespace IRISChatClient.Models
{
    public class RecoverModel
    {
        #region "Properties"
        public string RecoverUsername { get; set; }
        #endregion

        #region "Constructors"
        public RecoverModel()
        {
            RecoverUsername = "";
        }
        public RecoverModel(string RecoverUsername)
        {
            this.RecoverUsername = RecoverUsername;
        }
        #endregion
    }
}
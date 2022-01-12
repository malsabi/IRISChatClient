namespace IRISChatClient.Configs
{
    public class Constants
    {
        #region "User Session Constants"
        /// <summary>
        /// Represents the message when the user clicks on sign in button.
        /// </summary>
        public static readonly string ATTEMPT_SIGNIN_MESSAGE = "Attempting to sign in.";
        #endregion

        #region "Validation Constants"
        /// <summary>
        /// Represents the maximum length for a username to be entered.
        /// </summary>
        public static readonly int MAXIMUM_USERNAME_LENGTH = 20;
        /// <summary>
        /// Represents the maximum length for a password to be entered.
        /// </summary>
        public static readonly int MAXIMUM_PASSWORD_LENGTH = 30;
        /// <summary>
        /// Represents the maximum length for a first name to be entered.
        /// </summary>
        public static readonly int MAXIMUM_FIRSTNAME_LENGTH = 25;
        /// <summary>
        /// Represents the maximum length for a last name to be entered.
        /// </summary>
        public static readonly int MAXIMUM_LASTNAME_LENGTH = 25;
        /// <summary>
        /// Represents the maximum length for a email to be entered.
        /// </summary>
        public static readonly int MAXIMUM_EMAIL_LENMGTH = 100;
        /// <summary>
        /// Represents the empty value for a gender or can be an empty string.
        /// </summary>
        public static readonly string GENDER_NULL_VALUE = "N/A";
        #endregion

        #region "Status Constants"
        /// <summary>
        /// Represents the maximum show time for the status block.
        /// </summary>
        public static int MAXIMUM_SHOW_SECONDS = 5;
        #endregion

        #region "AES-256 CONSTANTS"
        /// <summary>
        /// Represents the Advanced Encryption Standard Key for encrypting/decrypting.
        /// </summary>
        public static readonly string AES_KEY = "x!A%D*G-KaPdSgVkYp3s6v9y$B?E(H+M";
        /// <summary>
        /// Represents the Initialization vector that is used to salt the encryption/decryption
        /// for more complexity.
        /// </summary>
        public static readonly string AES_IV = "15CV1/ZOnVI3rY4wk4INBg==";
        #endregion

        #region "SOCKET CONSTANTS"
        /// <summary>
        /// Represents the header size that is 4 bytes.
        /// </summary>
        public static readonly uint HEADER_SIZE = 4;
        /// <summary>
        /// Represents the maximum message size that the server can receive
        /// 1024 = 1KB, 1024 * 1024 = 1MB, etc.
        /// </summary>
        public static readonly int MAXIMUM_MESSAGE_SIZE = 1024 * 1024;
        /// <summary>
        /// Tries to reconnect each time after 5 seconds.
        /// 1000ms = 1 second, 1000 * 5 = 5 seconds.
        /// </summary>
        public static readonly int RECONNECT_DELAY = 1000 * 5;
        #endregion
    }
}
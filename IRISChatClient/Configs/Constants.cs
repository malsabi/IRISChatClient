namespace IRISChatClient.Configs
{
    public class Constants
    {
        #region "User Session Constants"
        /// <summary>
        /// Represents the message when the user clicks on sign in button.
        /// </summary>
        public static readonly string ATTEMPT_SIGNIN_MESSAGE = "Attempting to sign in..";
        /// <summary>
        /// Represents the message when the response is timed out.
        /// </summary>
        public static readonly string FAILED_RESPONSE_MESSAGE = "Failed to retrieve a response from the server.";
        /// <summary>
        /// Represents the message when the user clicks on register button.
        /// </summary>
        public static readonly string ATTEMPT_REGISTER_MESSAGE = "Attempting to register..";
        /// <summary>
        /// Represents the message when the user clicks on save changes button.
        /// </summary>
        public static readonly string ATTEMPT_UPDATE_MESSAGE = "Attempting to update user profile..";
        /// <summary>
        /// Represents the message when the user clicks on sign out button.
        /// </summary>
        public static readonly string ATTEMPT_SIGNOUT_MESSAGE = "Attempting to sign out..";
        /// <summary>
        /// Represents the message when the user clicks on delete account button.
        /// </summary>
        public static readonly string ATTEMPT_DELETE_MESSAGE = "Attempting to delete the account and all the associated data..";
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
        /// Represents a message when the client is attempting to reconnect.
        /// </summary>
        public static readonly string ATTEMPT_TO_RECONNECT_MESSAGE = "Failed to connect to the server, attempting to reconnect..";
        /// <summary>
        /// Represents a message when the client is connected.
        /// </summary>
        public static readonly string CONNECTED_MESSAGE = "Client is connected to server.";
        /// <summary>
        /// Represents the host or ip address that the client will be connected to.
        /// </summary>
        public static readonly string HOST = "127.0.0.1";
        /// <summary>
        /// Represents the port that is mapped to the server port.
        /// </summary>
        public static readonly string PORT = "1669";
        /// <summary>
        /// Represents if the client will attempt to reconnect after it is disconnected.
        /// </summary>
        public static readonly bool ATTEMPT_TO_RECONNECT = true;
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
        /// <summary>
        /// Represents the maximum response time out which is 3 seconds. 
        /// </summary>
        public static readonly int RESPONSE_TIME_OUT = 5;
        /// <summary>
        /// Represents the response monitor to check on timed out responses.
        /// The value is 100 milliseconds.
        /// </summary>
        public static readonly int RESPONSE_MONITOR_INTERVAL = 100;
        #endregion
    }
}
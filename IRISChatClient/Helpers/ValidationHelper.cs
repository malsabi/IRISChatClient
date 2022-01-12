using System.Linq;
namespace IRISChatClient.Helpers
{
    public class ValidationHelper
    {
        #region "Static ReadOnly Fields"
        public static readonly char[] IllegalCharacters = { '@', '/', '-', '(', ')', '&', '$', '^', ',', '=', '-', '+', ' '};
        #endregion

        #region "Static Methods"
        public static bool IsNameValid(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < Name.Length;i++)
                {
                    if (!char.IsLetter(Name[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool IsUsernameValid(string Username)
        {
            if (string.IsNullOrEmpty(Username))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < Username.Length; i++)
                {
                    if (!char.IsLetter(Username[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool IsPasswordValid(string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < IllegalCharacters.Length; i++)
                {
                    if (Password.Any(e => e.Equals(IllegalCharacters[i])))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

    }
}
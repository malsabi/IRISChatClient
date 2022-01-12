using System.Threading.Tasks;
using IRISChatClient.Interfaces;
using IRISChatClient.Models;
using IRISChatClient.Services.ServiceResults;

namespace IRISChatClient.Services
{
    public class UserSessionService : IUserSessionService
    {
        #region "Properties"
        public bool IsSignedIn { get; private set; }
        public bool IsSignedOut { get; private set; }
        public ProfileModel UserProfile { get; private set; }
        #endregion

        #region "Constructors"
        public UserSessionService()
        {
            IsSignedIn = false;
            IsSignedOut = true;
            UserProfile = null;
        }
        #endregion

        #region "Public Methods"
        public Task<SessionResult> DeleteAccount()
        {
            throw new System.NotImplementedException();
        }
        public async Task<SessionResult> SignIn(string Username, string Password, bool IsStaySignedIn)
        {
            SessionResult Result = new SessionResult("Successfully signed in", true);
            IsSignedIn = true;
            IsSignedOut = false;
            await Task.Delay(3000);
            return Result;
        }
        public Task<SessionResult> SignOut()
        {
            throw new System.NotImplementedException();
        }
        public Task<SessionResult> UpdateProfile()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
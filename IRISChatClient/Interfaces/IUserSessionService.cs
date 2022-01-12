using System.Threading.Tasks;
using IRISChatClient.Models;
using IRISChatClient.Services.ServiceResults;

namespace IRISChatClient.Interfaces
{
    /// <summary>
    /// TODO::
    /// Add Recover method that will do the recover mechanisim for changing the user password.
    /// </summary>
    public interface IUserSessionService
    {
        #region "User Session Properties"
        /// <summary>
        /// Returns true if the user is signed in otherwise false.
        /// </summary>
        bool IsSignedIn { get; }

        /// <summary>
        /// Returns true if the user is signed out otherwise false.
        /// </summary>
        bool IsSignedOut { get; }

        /// <summary>
        /// Returns the user profile if the user is signed in otherwise null.
        /// </summary>
        ProfileModel UserProfile { get; }
        #endregion

        #region "User Session Methods"
        /// <summary>
        /// Signs in the user and returns a task of <see cref="SessionResult"/>
        /// </summary>
        /// <param name="Username">Represents the unique user name that is registered.</param>
        /// <param name="Password">Represents the user password used to sign in.</param>
        /// <param name="IsStaySignedIn">Represents whether to stay signed in until the user signs out.</param>
        /// <returns>Returns <see cref="SessionResult"/> that represents the result operation of the sign in method.</returns>
        Task<SessionResult> SignIn(string Username, string Password, bool IsStaySignedIn);

        /// <summary>
        /// Updates the user profile in case if the user changes any of the data fields and returns a task of <see cref="SessionResult"/>.
        /// </summary>
        /// <returns>Returns <see cref="SessionResult"/> that represents the result operation of the update profile method.</returns>
        Task<SessionResult> UpdateProfile();

        /// <summary>
        /// Signs out the user in case if the user is signed in and returns a task of <see cref="SessionResult"/>.
        /// </summary>
        /// <returns>Returns <see cref="SessionResult"/> that represents the result operation of the sign out method.</returns>
        Task<SessionResult> SignOut();

        /// <summary>
        /// Deletes the user account in case if the user is signed in and returns a task of <see cref="SessionResult"/>.
        /// </summary>
        /// <returns>Returns <see cref="SessionResult"/> that represents the result operation of the delete account method.</returns>
        Task<SessionResult> DeleteAccount();
        #endregion
    }
}
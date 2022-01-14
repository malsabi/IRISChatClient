using System.Threading.Tasks;
using IRISChatClient.Models;
using IRISChatClient.Services.ServiceResults;
using Microsoft.Toolkit.Mvvm.Input;

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
        /// Returns true if the user is in the signing in process otherwise false.
        /// </summary>
        bool IsSigningIn { get; }

        /// <summary>
        /// Returns true if the user is signed in otherwise false.
        /// </summary>
        bool IsSignedIn { get; }

        /// <summary>
        /// Returns true if the user is in the signing out process otherwise false.
        /// </summary>
        bool IsSigningOut { get; }

        /// <summary>
        /// Returns true if the user is signed out otherwise false.
        /// </summary>
        bool IsSignedOut { get; }

        /// <summary>
        /// Returns the user profile if the user is signed in otherwise null.
        /// </summary>
        ProfileModel UserProfile { get; }

        /// <summary>
        /// Used for notifying a command when a property is changed in the <see cref="IUserSessionService"/>
        /// </summary>
        IRelayCommand NotifyCommand { get; }

        /// <summary>
        /// Used for request/response to/from server, the <see cref="IClientService"/> is used for <seealso cref="Services.ClientService"/>
        /// </summary>
        IClientService Client { get; }
        #endregion

        #region "User Session Methods"
        /// <summary>
        /// Registers the user and returns a task of <see cref="SessionResult"/>
        /// </summary>
        /// <param name="UserRegisterProfile">Represents the information entered by the user for registeration.</param>
        /// <returns>Returns <see cref="SessionResult"/> that represents the result operation of the register method.</returns>
        Task<SessionResult> Register(RegisterModel UserRegisterProfile);

        /// <summary>
        /// Signs in the user and returns a task of <see cref="SessionResult"/>
        /// </summary>
        /// <param name="Username">Represents the unique user name that is registered.</param>
        /// <param name="Password">Represents the user password used to sign in.</param>
        /// <param name="IsStaySignedIn">Represents whether to stay signed in until the user signs out.</param>
        /// <returns>Returns <see cref="SessionResult"/> that represents the result operation of the sign in method.</returns>
        Task<SessionResult> SignIn(SignInModel UserSignInInformation);

        /// <summary>
        /// Updates the user profile in case if the user changes any of the data fields and returns a task of <see cref="SessionResult"/>.
        /// </summary>
        /// <returns>Returns <see cref="SessionResult"/> that represents the result operation of the update profile method.</returns>
        /// <param name="OldUsername">Represents the old username.</param>
        /// <param name="UpdatedUserProfile">Represents the updated profile that is stored in <see cref="ProfileModel"/></param>
        Task<SessionResult> UpdateProfile(string OldUsername, ProfileModel UpdatedUserProfile);

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

        #region "Notify Command Methods"
        /// <summary>
        /// Sets the <see cref="NotifyCommand"/> Property to the NotifyCommand Parameter./>
        /// </summary>
        /// <param name="NotifyCommand">Represents a command to be notified when a property is changed.</param>
        void SetCommand(IRelayCommand NotifyCommand);
        #endregion
    }
}
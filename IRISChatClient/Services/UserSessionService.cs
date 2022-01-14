using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using IRISChatClient.Models;
using IRISChatClient.Interfaces;
using IRISChatClient.Networking.Messages;
using IRISChatClient.Services.ServiceResults;
using IRISChatClient.Configs;
using IRISChatClient.Validations;

namespace IRISChatClient.Services
{
    public class UserSessionService : IUserSessionService
    {
        #region "Fields"
        private bool isSigningIn;
        private bool isSignedIn;
        private bool isSigningOut;
        private bool isSignedOut;
        private ProfileModel userProfile;
        #endregion

        #region "Properties"
        public bool IsSigningIn
        {
            get => isSigningIn;
            private set
            {
                isSigningIn = value;
                NotifyCommand?.NotifyCanExecuteChanged();
            }
        }

        public bool IsSignedIn
        {
            get => isSignedIn;
            private set
            {
                isSignedIn = value;
                NotifyCommand?.NotifyCanExecuteChanged();
            }
        }

        public bool IsSigningOut 
        {
            get => isSigningOut;
            private set
            {
                isSigningOut = value;
                NotifyCommand?.NotifyCanExecuteChanged();
            }
        }

        public bool IsSignedOut
        {
            get => isSignedOut;
            private set
            {
                isSignedOut = value;
                NotifyCommand?.NotifyCanExecuteChanged();
            }
        }

        public ProfileModel UserProfile 
        {
            get => userProfile;
            private set
            {
                userProfile = value;
                NotifyCommand?.NotifyCanExecuteChanged();
            }
        }

        public IRelayCommand NotifyCommand { get; private set; }

        public IClientService Client { get; private set; }
        #endregion

        #region "Constructors"
        public UserSessionService()
        {
            Client = App.GetService<IClientService>();
        }
        #endregion

        #region "User Session Methods"
        public async Task<SessionResult> Register(RegisterModel UserRegisterInformation)
        {
            SessionResult Result = new SessionResult();
            try
            {
                ValidationResult Validation = RegisterValidator.Invalidate(UserRegisterInformation);

                if (Validation.IsOperationSuccess)
                {
                    RegisterUserMessage RegisterMessage = new RegisterUserMessage(UserRegisterInformation);

                    IResponse Response = await Client.SendMessage(RegisterMessage, typeof(RegisterUserResultMessage));

                    if (Response.IsTimedout)
                    {
                        Result.Message = Constants.FAILED_RESPONSE_MESSAGE;
                    }
                    else
                    {
                        RegisterUserResultMessage RegisterResultMessage = (RegisterUserResultMessage)Response.Result;
                        Result.Message = RegisterResultMessage.ResultMessage;
                        if (RegisterResultMessage.IsResultSuccess)
                        {
                            Result.IsSuccess = true;
                        }
                        else
                        {
                            Result.IsSuccess = false;
                        }
                    }
                }
                else
                {
                    Result.Message = Validation.Message;
                }
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
            }
            return Result;
        }

        public async Task<SessionResult> SignIn(SignInModel UserSignInInformation)
        {
            SessionResult Result = new SessionResult();
            IsSigningIn = true;
            try
            {
                ValidationResult Validation = SignInValidator.Invalidate(UserSignInInformation);

                if (Validation.IsOperationSuccess)
                {
                    SignInUserMessage SignInMessage = new SignInUserMessage(UserSignInInformation);

                    IResponse Response = await Client.SendMessage(SignInMessage, typeof(SignInUserResultMessage));

                    if (Response.IsTimedout)
                    {
                        Result.Message = Constants.FAILED_RESPONSE_MESSAGE;
                    }
                    else
                    {
                        SignInUserResultMessage SignInResultMessage = (SignInUserResultMessage)Response.Result;
                        Result.Message = SignInResultMessage.ResultMessage;
                        if (SignInResultMessage.IsResultSuccess)
                        {
                            Result.IsSuccess = true;
                            UserProfile = new ProfileModel(SignInResultMessage.UserProfile);
                            IsSignedIn = true;
                        }
                        else
                        {
                            Result.IsSuccess = false;
                        }
                    }
                }
                else
                {
                    Result.Message = Validation.Message;
                }
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
            }
            IsSigningIn = false;
            return Result;
        }

        public async Task<SessionResult> UpdateProfile(string OldUsername, ProfileModel UpdatedUserProfile)
        {
            SessionResult Result = new SessionResult();
            try
            {
                ValidationResult Validation = UpdateProfileValidator.Invalidate(UpdatedUserProfile);

                if (Validation.IsOperationSuccess)
                {
                    UpdateUserProfileMessage UpdateProfileMessage = new UpdateUserProfileMessage()
                    {
                        OldUsername = OldUsername,
                        UpdatedUserProfile = new ProfileModel(UpdatedUserProfile)
                    };

                    IResponse Response = await Client.SendMessage(UpdateProfileMessage, typeof(UpdateUserProfileResultMessage));

                    if (Response.IsTimedout)
                    {
                        Result.Message = Constants.FAILED_RESPONSE_MESSAGE;
                    }
                    else
                    {
                        UpdateUserProfileResultMessage UpdateProfileResultMessage = (UpdateUserProfileResultMessage)Response.Result;
                        Result.Message = UpdateProfileResultMessage.ResultMessage;
                        if (UpdateProfileResultMessage.IsResultSuccess)
                        {
                            UserProfile = new ProfileModel(UpdateProfileResultMessage.UpdatedUserProfile);
                            Result.IsSuccess = true;
                        }
                        else
                        {
                            Result.IsSuccess = false;
                        }
                    }
                }
                else
                {
                    Result.Message = Validation.Message;
                }
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
            }
            return Result;
        }

        public async Task<SessionResult> SignOut()
        {
            SessionResult Result = new SessionResult();
            try
            {
                SignOutUserMessage SignOutMessage = new SignOutUserMessage()
                {
                     Username = UserProfile.Username
                };

                IResponse Response = await Client.SendMessage(SignOutMessage, typeof(SignOutUserResultMessage));

                if (Response.IsTimedout)
                {
                    Result.Message = Constants.FAILED_RESPONSE_MESSAGE;
                }
                else
                {
                    SignOutUserResultMessage SignOutResultMessage = (SignOutUserResultMessage)Response.Result;
                    Result.Message = SignOutResultMessage.ResultMessage;
                    if (SignOutResultMessage.IsResultSuccess)
                    {
                        Result.IsSuccess = true;
                    }
                    else
                    {
                        Result.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
            }
            return Result;
        }

        public async Task<SessionResult> DeleteAccount()
        {
            SessionResult Result = new SessionResult();
            try
            {
                DeleteUserAccountMessage DeleteAccountMessage = new DeleteUserAccountMessage()
                {
                    Username = UserProfile.Username
                };

                IResponse Response = await Client.SendMessage(DeleteAccountMessage, typeof(DeleteUserAccountResultMessage));

                if (Response.IsTimedout)
                {
                    Result.Message = Constants.FAILED_RESPONSE_MESSAGE;
                }
                else
                {
                    DeleteUserAccountResultMessage DeleteAccountResultMessage = (DeleteUserAccountResultMessage)Response.Result;
                    Result.Message = DeleteAccountResultMessage.ResultMessage;
                    if (DeleteAccountResultMessage.IsResultSuccess)
                    {
                        Result.IsSuccess = true;
                    }
                    else
                    {
                        Result.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
            }
            return Result;
        }
        #endregion

        #region "Notify Command Methods"
        public void SetCommand(IRelayCommand NotifyCommand)
        {
            this.NotifyCommand = NotifyCommand;
        }
        #endregion
    }
}
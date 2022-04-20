using GreenLineSystems.Core.ViewModels;

namespace GreenLineSystems.Core.Interfaces;

public interface IUserService
{
    Task<MessageResult<bool>> AddUser(UserCreationRequest model);
    
    Task<MessageResult<LoginResponse>> LoginUser(LoginModel model);
    
    Task<MessageResult<bool>> ResetCredentials(UserCredentialsUpdate model);
}
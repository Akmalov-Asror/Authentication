using Teaching.V1.Auth.Models.AuthModels;

namespace Teaching.V1.Auth.Services.Interfaces;

public interface IAuthService
{
    ValueTask<UserModel> Registration(RegisterModel user);
    ValueTask<TokenModel> Login(LoginModel model);
}

using Teaching.V1.Auth.Models.AuthModels;

namespace Teaching.V1.Auth.CQRS.Services;

public interface IAuthServiceWithCQRS
{
    ValueTask<UserModel> Registration(RegisterModel user);
    ValueTask<TokenModel> Login(LoginModel model);
}

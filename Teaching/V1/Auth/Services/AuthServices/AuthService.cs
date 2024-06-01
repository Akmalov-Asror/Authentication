using Microsoft.AspNetCore.Identity;
using Teaching.Common.Entities.Users;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenRepository _tokenGenerator;
    private readonly SignInManager<User> _signInManager;
    public AuthService(ITokenRepository tokenGenerator, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _tokenGenerator = tokenGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async ValueTask<TokenModel> Login(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            throw new UserException(400, "user_not_Found");
        }
        var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
        var roles = await _userManager.GetRolesAsync(user);
        if (!checkPassword)
        {
            throw new UserException(401, "Email or password is incorrect");
        }
        var token = _tokenGenerator.CreateToken(user, roles);
        return new TokenModel { Token = token };

    }

    public async ValueTask<UserModel> Registration(RegisterModel user)
    {
        User newUser = new User()
        {
            UserName = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,

        };
        var registerUser = await _userManager.CreateAsync(newUser, user.Password);
        if (!registerUser.Succeeded)
        {
            throw new UserException(400, string.Join(", ", registerUser.Errors.Select(x => x.Description)));
        }
        return new UserModel().MapFromEntity(newUser);
    }
}

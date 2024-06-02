using MediatR;
using Microsoft.AspNetCore.Identity;
using Teaching.Common.Entities.Users;
using Teaching.V1.Auth.CQRS.Commands;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.CQRS.CommandHandlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenModel>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenRepository _tokenGenerator;
    private readonly SignInManager<User> _signInManager;

    public LoginCommandHandler(UserManager<User> userManager, ITokenRepository tokenGenerator, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _signInManager = signInManager;
    }

    public async Task<TokenModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UserException(400, "user_not_Found");
        }

        var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        var roles = await _userManager.GetRolesAsync(user);

        if (!checkPassword)
        {
            throw new UserException(401, "Email or password is incorrect");
        }

        var token = _tokenGenerator.CreateToken(user, roles);
        return new TokenModel { Token = token };
    }
}

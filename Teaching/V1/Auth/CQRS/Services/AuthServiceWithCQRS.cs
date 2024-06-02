using MediatR;
using Teaching.V1.Auth.CQRS.Commands;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.CQRS.Services;

internal sealed class AuthServiceWithCQRS : IAuthServiceWithCQRS
{
    private readonly IMediator _mediator;

    public AuthServiceWithCQRS(IMediator mediator) => _mediator = mediator;

    public async ValueTask<TokenModel> Login(LoginModel model)
    {
        var command = new LoginCommand
        {
            Email = model.Email,
            Password = model.Password
        };

        return await _mediator.Send(command);
    }

    public async ValueTask<UserModel> Registration(RegisterModel user)
    {
        var command = new RegisterCommand
        {
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.Password
        };

        return await _mediator.Send(command);
    }
}

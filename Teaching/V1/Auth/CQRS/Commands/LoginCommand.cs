using MediatR;
using Teaching.V1.Auth.Models.AuthModels;

namespace Teaching.V1.Auth.CQRS.Commands;

public class LoginCommand : IRequest<TokenModel>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

using MediatR;
using Teaching.V1.Auth.Models.AuthModels;

namespace Teaching.V1.Auth.CQRS.Commands;

public class RegisterCommand : IRequest<UserModel>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
}

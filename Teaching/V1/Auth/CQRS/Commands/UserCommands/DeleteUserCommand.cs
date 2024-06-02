using MediatR;

namespace Teaching.V1.Auth.CQRS.Commands.UserCommands;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
}

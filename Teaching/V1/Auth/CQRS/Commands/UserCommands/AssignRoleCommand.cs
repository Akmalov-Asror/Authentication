using MediatR;
using Teaching.V1.Auth.Models.AuthModels;

namespace Teaching.V1.Auth.CQRS.Commands.UserCommands;

public class AssignRoleCommand : IRequest<UsersModel>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

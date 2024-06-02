using MediatR;
using Teaching.V1.Auth.CQRS.Commands.UserCommands;
using Teaching.V1.Auth.CQRS.Queries.UserQueries;
using Teaching.V1.Auth.Models.AuthModels;

namespace Teaching.V1.Auth.CQRS.Services;

internal sealed class UserServiceCQRS : IUserServiceCQRS
{
    private readonly IMediator _mediator;

    public UserServiceCQRS(IMediator mediator) => _mediator = mediator;

    public async ValueTask<bool> DeleteUserAsync(Guid id)
    {
        var command = new DeleteUserCommand { UserId = id };
        return await _mediator.Send(command);
    }

    public async ValueTask<IList<UsersModel>> GetAllUsersAsync()
    {
        var query = new GetAllUsersQuery();
        return await _mediator.Send(query);
    }

    public async ValueTask<UsersModel> UserRoleAsync(UserRoleCreateModel model)
    {
        var command = new AssignRoleCommand { UserId = model.UserId, RoleId = model.RoleId };
        return await _mediator.Send(command);
    }

    public async ValueTask<UsersModel> RemoveRoleUserAsync(RemoveRoleFromUserModel model)
    {
        var command = new RemoveRoleCommand { UserId = model.UserId, RoleId = model.RoleId };
        return await _mediator.Send(command);
    }
}

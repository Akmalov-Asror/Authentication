using MediatR;
using Microsoft.AspNetCore.Identity;
using Teaching.Common.Entities.Users;
using Teaching.V1.Auth.CQRS.Commands.UserCommands;
using Teaching.V1.Auth.Services.Exceptions;

namespace Teaching.V1.Auth.CQRS.CommandHandlers.UserCommandHandler;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public DeleteUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            throw new UserException(404, "User not found");
        }

        await _userManager.DeleteAsync(user);
        return true;
    }
}

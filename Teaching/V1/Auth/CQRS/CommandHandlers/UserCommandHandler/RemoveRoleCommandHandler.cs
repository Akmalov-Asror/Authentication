using MediatR;
using Microsoft.AspNetCore.Identity;
using Teaching.Common.Entities.Users;
using Teaching.V1.Auth.CQRS.Commands.UserCommands;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;

namespace Teaching.V1.Auth.CQRS.CommandHandlers.UserCommandHandler;

public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, UsersModel>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RemoveRoleCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<UsersModel> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            throw new UserException(404, "User not found");
        }

        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null)
        {
            throw new UserException(404, "Role not found");
        }

        var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
        if (isInRole)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
            {
                throw new UserException(400, string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
        else
        {
            throw new UserException(400, "Role already removed from this user");
        }

        return new UsersModel().MapFromEntity(user);
    }
}

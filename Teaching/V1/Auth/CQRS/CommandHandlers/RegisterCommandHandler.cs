using MediatR;
using Microsoft.AspNetCore.Identity;
using Teaching.Common.Entities.Users;
using Teaching.V1.Auth.CQRS.Commands;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;

namespace Teaching.V1.Auth.CQRS.CommandHandlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserModel>
{
    private readonly UserManager<User> _userManager;

    public RegisterCommandHandler(UserManager<User> userManager) => _userManager = userManager;

    public async Task<UserModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var registerUser = await _userManager.CreateAsync(newUser, request.Password);
        if (!registerUser.Succeeded)
        {
            throw new UserException(400, string.Join(", ", registerUser.Errors.Select(x => x.Description)));
        }

        return new UserModel().MapFromEntity(newUser);
    }

}

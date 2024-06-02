using Azure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Teaching.Common.Entities.Users;
using Teaching.V1.Auth.CQRS.Queries.UserQueries;
using Teaching.V1.Auth.Models.AuthModels;

namespace Teaching.V1.Auth.CQRS.CommandHandlers.UserCommandHandler;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IList<UsersModel>>
//TResponse —> IList<Smth>
//TRequest —> SmthCommand 
{
    private readonly UserManager<User> _userManager;

    public GetAllUsersQueryHandler(UserManager<User> userManager)
        => _userManager = userManager;

    public async Task<IList<UsersModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync(cancellationToken);
        var usersList = new List<UsersModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            usersList.Add(new UsersModel().MapFromEntities(user, roles));
        }

        return usersList;
    }
}

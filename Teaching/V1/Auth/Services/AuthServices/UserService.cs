using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Teaching.Common.Entities.Users;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.Services.AuthServices;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    public UserService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async ValueTask<bool> DeleteUserAsync(Guid Id)
    {
        var findUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
        await _userManager.DeleteAsync(findUser);
        return true;
    }

    public async ValueTask<IList<UsersModel>> GetAllUsersAsync()
    {
        var userList = _userManager.Users.ToList();
        var usersList = new List<UsersModel>();
        foreach (var user in userList)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userWithRole = new UsersModel().MapFromEntities(user, roles);
            usersList.Add(userWithRole);
        }

        return usersList;
    }


    public async ValueTask<UsersModel> UserRoleAsync(UserRoleCreateModel userRoleCreateModel)
    {
        var role = await _roleManager.FindByIdAsync(userRoleCreateModel.RoleId.ToString());
        if (role == null)
        {
            throw new UserException(404, "role not found");
        }
        var user = await _userManager.FindByIdAsync(userRoleCreateModel.UserId.ToString());

        var securityStamp = Guid.NewGuid().ToString();
        await _userManager.UpdateSecurityStampAsync(user);


        if (user is null)
        {
            throw new UserException(404, "user not found");
        }
        var isinRole = await _userManager.IsInRoleAsync(user, role.Name);
        if (!isinRole)
        {
            var rolecreateResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!rolecreateResult.Succeeded)
            {
                throw new UserException(400, string.Join(", ", rolecreateResult.Errors.Select(x => x.Description)));
            }
        }
        else
        {
            throw new UserException(400, "role_already_added_to_this_user");
        }
        return new UsersModel();

    }
    public async ValueTask<UsersModel> RemoveRoleUserAsync(RemoveRoleFromUserModel removeRoleFromUserModel)
    {
        var role = await _roleManager.FindByIdAsync(removeRoleFromUserModel.RoleId.ToString());
        if (role == null)
        {
            throw new UserException(404, "role not found");
        }
        var user = await _userManager.FindByIdAsync(removeRoleFromUserModel.UserId.ToString());
        if (user is null)
        {
            throw new UserException(404, "user not found");
        }
        var isinRole = await _userManager.IsInRoleAsync(user, role.Name);
        if (isinRole)
        {
            var rolecreateResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!rolecreateResult.Succeeded)
            {
                throw new UserException(400, string.Join(", ", rolecreateResult.Errors.Select(x => x.Description)));
            }
        }
        else
        {
            throw new UserException(400, "role_already_remove_to_this_user");
        }
        return new UsersModel().MapFromEntity(user);
    }
}

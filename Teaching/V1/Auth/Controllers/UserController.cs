using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService) 
        => _userService = userService;

    [HttpGet]
    public async ValueTask<IActionResult> GetAll()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }

    [HttpDelete("DeleteUser")]
    [Authorize(Roles = "SuperAdmin")]
    public async ValueTask<IActionResult> Detele(Guid id)
    {
        return Ok(await _userService.DeleteUserAsync(id));
    }

    [HttpPost("UserRoleCreate")]
    public async ValueTask<IActionResult> RoleCreate([FromForm] UserRoleCreateModel model)
    {
        try
        {

            return Ok(await _userService.UserRoleAsync(model));
        }
        catch (UserException pr)
        {
            return BadRequest(new
            {
                global = pr.Message,
            });
        }
    }

    [HttpDelete("RemoveRoleUserAsync")]
    public async ValueTask<IActionResult> RemoveRoleUserAsync([FromForm] RemoveRoleFromUserModel removeRoleFromUserModel)
    {
        try
        {

            return Ok(await _userService.RemoveRoleUserAsync(removeRoleFromUserModel));
        }
        catch (UserException ex)
        {
            return BadRequest(new
            {
                global = ex.Message,
            });
        }
    }
}

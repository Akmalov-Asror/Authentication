using Microsoft.AspNetCore.Mvc;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) 
        => _authService = authService;

    [HttpPost("Login")]
    public async ValueTask<IActionResult> Login([FromForm] LoginModel model)
    {
        try
        {

            return Ok(await _authService.Login(model));
        }
        catch (UserException ex)
        {
            return BadRequest(new
            {
                global = ex.Message,
            });
        }
    }
    [HttpPost("register")]
    public async ValueTask<IActionResult> Register([FromForm] RegisterModel model)
    {
        try
        {

            return Ok(await _authService.Registration(model));
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

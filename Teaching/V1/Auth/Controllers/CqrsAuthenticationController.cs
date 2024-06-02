using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teaching.V1.Auth.CQRS.Services;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
//[Obsolete("This API version is deprecated. Use API version 1.0 instead.")]
public class CqrsAuthenticationController : ControllerBase
{
    private readonly IAuthServiceWithCQRS _authService;

    public CqrsAuthenticationController(IAuthServiceWithCQRS authService)
        => _authService = authService;

    [HttpPost("LoginCQRS")]
    [MapToApiVersion("1.0")]
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
    [MapToApiVersion("1.0")]
    [HttpPost("registerCQRS")]
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

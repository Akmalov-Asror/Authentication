﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Exceptions;
using Teaching.V1.Auth.Services.Interfaces;

namespace Teaching.V1.Auth.Controllers;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) 
        => _authService = authService;

    [HttpPost("Login")]
    [MapToApiVersion("2.0")]
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

    [SwaggerOperation(Summary = "You can get delist of cars", Description = "you should smile more")]
    [HttpPost("register")]
    [MapToApiVersion("2.0")]
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

/*ApiVersionAttribute
MapToApiVersionAttribute
ApiVersionNeutralAttribute
DeprecatedAttribute
AdvertiseApiVersionsAttribute*/
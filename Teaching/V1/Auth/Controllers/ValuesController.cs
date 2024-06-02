using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Teaching.V1.Auth.CQRS.Services;

namespace Teaching.V1.Auth.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IUserServiceCQRS _userServiceCQRS;

    public ValuesController(IUserServiceCQRS userServiceCQRS) 
        => _userServiceCQRS = userServiceCQRS;

    [HttpGet]
    [MapToApiVersion("1.0")]
    [SwaggerOperation(Summary = "Get information", Description = "Retrieve information about something.")]
    public async ValueTask<IActionResult> Get() 
        => Ok(await _userServiceCQRS.GetAllUsersAsync());
}

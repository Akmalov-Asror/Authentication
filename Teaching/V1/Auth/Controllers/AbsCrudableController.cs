using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Teaching.Common.Absractions;
using Teaching.Common.Entities.Users;
using Teaching.Common.UnitOfWork.Implementation;
using Teaching.Common.UnitOfWork.Interfaces;

namespace Teaching.V1.Auth.Controllers;

[ApiController]
[ApiVersion("4.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class
    AbsCrudableController<TIdentity, TEntity, TCreateModel, TUpdateModel, TDetailModel, TRowModel> : ControllerBase
    where TEntity : Auditable
    where TIdentity : BaseIdentityModel
{

    #region Fields

    private readonly AbsCrudable<TIdentity, TEntity, TCreateModel, TUpdateModel, TDetailModel, TRowModel> _service;

    #endregion

    #region Ctors

    public AbsCrudableController(AbsCrudable<TIdentity,
        TEntity, TCreateModel, TUpdateModel, TDetailModel, TRowModel> service)
        => _service = service;

    #endregion

    #region Endpoints

    [HttpPost]
    [MapToApiVersion("4.0")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status400BadRequest)]
    public async ValueTask<IActionResult> CreateAsync([FromBody] TCreateModel model)
        => Ok(await _service.CreateAsync(model));

    [HttpGet("/{id}")]
    [MapToApiVersion("4.0")]
    public async ValueTask<ActionResult<TDetailModel>> DetailAsync(TIdentity id)
        => Ok(await _service.GetDetailAsync(id));

    [HttpPatch("/{id}")]
    [MapToApiVersion("4.0")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status400BadRequest)]
    public async ValueTask<ActionResult<TIdentity>> UpdateAsync([FromRoute]TIdentity id, [FromBody] TUpdateModel updateModel)
        => Ok(await _service.UpdateAsync(id, updateModel));

    [HttpDelete("/{id}")]
    [MapToApiVersion("4.0")]
    public async ValueTask<ActionResult> DeleteAsync(TIdentity id)
        => new JsonResult(await _service.DeleteAsync(id));

    #endregion
}


/// <summary>
/// generic controller that gets smth
/// </summary>
[ApiVersion("4.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserseController : AbsCrudableController<BaseIdentityModel, Users, UserCreateModel, UserCreateModel,
    UserDetailModel, UserRowModel>
{

    #region Fields

    private readonly UserServiceAbs _service;

    #endregion

    public UserseController(UserServiceAbs service) : base(service)
    {
        _service = service;
    }
}

#region Models & entities

public class Users : Auditable
{
    public string Name { get; set; }
}

public class UserCreateModel
{ }

public class UserDetailModel { }

public class UserRowModel { }

public class BaseIdentityModel
{
    public Guid Id { get; set; }
}

#endregion


public class UserServiceAbs
    : AbsCrudable<BaseIdentityModel, Users, UserCreateModel, UserCreateModel, UserDetailModel, UserRowModel>
{
    public readonly IMapper _mapper;
    private readonly IRepositoryAbs<Users> _repository;

    public UserServiceAbs(IRepositoryAbs<Users> repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    //you can override your services methods
}

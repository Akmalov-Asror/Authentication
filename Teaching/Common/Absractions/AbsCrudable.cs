using AutoMapper;
using Teaching.Common.Entities.Users;
using Teaching.Common.UnitOfWork.Implementation;
using Teaching.Common.UnitOfWork.Interfaces;
using Teaching.V1.Auth.Controllers;

namespace Teaching.Common.Absractions;

public abstract class AbsCrudable<TIdentity, TEntity, TCreateModel, TUpdateModel, TDetailModel, TRowModel> :
    ICrudable<TIdentity, TCreateModel, TUpdateModel, TDetailModel>
    where TEntity : Auditable
    where TIdentity : BaseIdentityModel
{
    private readonly IRepositoryAbs<TEntity> _repository;
    private readonly IMapper _mapper;

    protected AbsCrudable(IRepositoryAbs<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<TIdentity> CreateAsync(TCreateModel createModel)
    {
        var mappedCreateModel = _mapper.Map<TEntity>(createModel);
        var entityId = await _repository.AddAsync(mappedCreateModel);
        return (TIdentity)(object)entityId;
    }

    public virtual async Task<TIdentity> UpdateAsync(TIdentity id, TUpdateModel model)
    {
        TEntity existingEntity = await _repository.GetByIdAsync((Guid)(object)id, throwOnNotFound: true);
        existingEntity = _mapper.Map<TEntity>(model);
        await _repository.UpdateAsync(existingEntity);
        return id;
    }

    public virtual async Task<bool> DeleteAsync(TIdentity id)
    {
        var isDeleted = await _repository.DeleteAsync((Guid)(object)id);
        return isDeleted;
    }

    public virtual async Task<TDetailModel> GetDetailAsync(TIdentity id)
    {
        var entity = await _repository.GetByIdAsync((Guid)(object)id, throwOnNotFound: true);
        return _mapper.Map<TDetailModel>(entity);
    }
}

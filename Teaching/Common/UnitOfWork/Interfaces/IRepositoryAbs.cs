using System.Linq.Expressions;
using Teaching.Common.Entities.Users;

namespace Teaching.Common.UnitOfWork.Interfaces;

public interface IRepositoryAbs<TEntity> where TEntity : Auditable
{
    IQueryable<TEntity> GetAll();
    IQueryable<TEntity> GetAllByExp(Expression<Func<TEntity, bool>> predicate);
    Task<Guid> AddAsync(TEntity entity);
    Task<Guid[]> AddRangeAsync(List<TEntity> entities);
    Task<Guid> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> DeleteRangeAsync(Guid[] entityIdArr);

    Task<TEntity> GetByIdExpAsync(Guid id,
        Expression<Func<TEntity, bool>> predicate,
        bool throwOnNotFound = false);

    Task<TEntity> GetByIdAsync(Guid id,
        bool throwOnNotFound = false);
}

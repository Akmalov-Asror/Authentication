using Teaching.Common.Entities.Users;

namespace Teaching.Common.UnitOfWork.Implementation;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : Auditable;
    Task<int> SaveChangesAsync();
}

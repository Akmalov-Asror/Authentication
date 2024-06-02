using Microsoft.EntityFrameworkCore;
using Teaching.Common.Entities.Users;
using Teaching.Common.UnitOfWork.Interface;

namespace Teaching.Common.UnitOfWork.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : Auditable
    {
        if (!_repositories.ContainsKey(typeof(TEntity)))
        {
            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
        }
        return (IRepository<TEntity>)_repositories[typeof(TEntity)];
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}


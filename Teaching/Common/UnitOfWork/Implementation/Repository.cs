using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using Teaching.Common.Entities.Users;
using Teaching.Common.UnitOfWork.Implementation;

namespace Teaching.Common.UnitOfWork.Interface;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual IQueryable<T> GetAll(
        Expression<Func<T, bool>> expression,
        string[] includes = null,
    bool isTracking = true
    )
    {
        var query = expression is null ? _dbSet : _dbSet.Where(expression);

        if (includes != null)
            foreach (var include in includes)
                if (!string.IsNullOrEmpty(include))
                    query = query.Include(include);

        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public virtual async ValueTask<T> GetAsync(
        Expression<Func<T, bool>> expression,
        bool isTracking = true,
        string[] includes = null
    ) => await GetAll(expression, includes, isTracking).FirstOrDefaultAsync();

    public async ValueTask<T> CreateAsync(T entity)
    {
        var entityData = (await _context.AddAsync(entity)).Entity;


        return entityData;

    }

    public async ValueTask<bool> DeleteAsync(Guid id)
    {
        var entity = await GetAsync(e => e.Id == id);

        if (entity == null)
            return false;
        _dbSet.Remove(entity);
        return true;
    }

    public T Update(T entity) => _dbSet.Update(entity).Entity;

    public async Task BulkInsertAsync(IEnumerable<T> entities)
    {
        await _context.BulkInsertAsync(entities.ToList());
    }

    public async Task BulkUpdateAsync(IEnumerable<T> entities)
    {
        await _context.BulkUpdateAsync(entities.ToList());
    }

    public async Task BulkDeleteAsync(IEnumerable<T> entities)
    {
        await _context.BulkDeleteAsync(entities.ToList());
    }
}


using System.Linq.Expressions;
using Teaching.Common.Entities.Users;

namespace Teaching.Common.UnitOfWork.Implementation;

public interface IRepository<T> where T : Auditable
{
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true);
    ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, bool isTracking = true, string[] includes = null);
    ValueTask<T> CreateAsync(T entity);
    ValueTask<bool> DeleteAsync(Guid id);
    T Update(T entity);
    Task BulkInsertAsync(IEnumerable<T> entities);
    Task BulkUpdateAsync(IEnumerable<T> entities);
    Task BulkDeleteAsync(IEnumerable<T> entities);
}

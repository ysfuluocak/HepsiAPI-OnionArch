using HepsiAPI.Domain.Common;
using System.Linq.Expressions;


namespace HepsiAPI.Application.Interfaces.Repositories
{
    public interface IReadRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, int pageNumber = 1, int pageSize = 10, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool enableTracking = false, params Expression<Func<TEntity, object>>[] includes);

        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}

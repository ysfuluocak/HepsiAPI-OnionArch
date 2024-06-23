using HepsiAPI.Domain.Common;
using System.Linq.Expressions;


namespace HepsiAPI.Application.Interfaces.Repositories
{
    public interface IReadRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
    {

        IQueryable<TEntity> AsQueryable();

        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> GetPaginatedListAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> GetSingleEntityAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity?> Get(
            Expression<Func<TEntity, bool>> predicate,
            bool noTracking = true,
            params Expression<Func<TEntity, object>>[] includes);

        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);


    }
}

using HepsiAPI.Application.Interfaces.Repositories;
using HepsiAPI.Domain.Common;
using HepsiAPI.Persistence.Context;
using HepsiAPI.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories
{
    public class EfReadRepository<TEntity> : IReadRepository<TEntity>
    where TEntity : class, IBaseEntity, new()
    {
        private readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        public EfReadRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (predicate is not null) query = query.Where(predicate);
            return await query.IncludeMultiple(includes).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber = 1, int pageSize = 10, Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (predicate is not null) query = query.Where(predicate);
            return await query.IncludeMultiple(includes).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<TEntity> GetAsync(bool enableTracking, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            query = enableTracking ? query : query.AsNoTracking();
            return await query.IncludeMultiple(includes).SingleOrDefaultAsync(predicate);
            //return await query.IncludeMultiple(includes).FirstOrDefaultAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }


    }
}

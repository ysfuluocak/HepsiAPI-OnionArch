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



        public IQueryable<TEntity> AsQueryable() => _dbSet.AsQueryable();

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (predicate is not null) query = query.Where(predicate);
            if (orderBy is not null) query = orderBy(query);
            var entities = await query.IncludeMultiple(includes).ToListAsync();
            return entities;
        }

        public async Task<List<TEntity>> GetPaginatedListAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (predicate is not null) query = query.Where(predicate);
            if (orderBy is not null) query = orderBy(query);
            var entities = await query.IncludeMultiple(includes).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return entities;
        }

        public async Task<TEntity?> GetSingleEntityAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            query = noTracking ? query.AsNoTracking() : query;
            var entity = await query.IncludeMultiple(includes).FirstOrDefaultAsync(predicate);
            //return await query.IncludeMultiple(includes).SingleOrDefaultAsync(predicate);
            return entity;
        }


        public IQueryable<TEntity?> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            query = noTracking ? query.AsNoTracking() : query;

            if (predicate != null)
                query = query.Where(predicate);

            query = query.IncludeMultiple(includes);

            return query;
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

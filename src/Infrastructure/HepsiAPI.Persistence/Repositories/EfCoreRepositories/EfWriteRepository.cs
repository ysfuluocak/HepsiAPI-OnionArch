using HepsiAPI.Application.Interfaces.Repositories;
using HepsiAPI.Domain.Common;
using HepsiAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories
{
    public class EfWriteRepository<TEntity> : IWriteRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EfWriteRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var addedEntities = _context.Entry(entities);
            addedEntities.State = EntityState.Added;
            return await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            //entity kontrolu eklenebilir
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

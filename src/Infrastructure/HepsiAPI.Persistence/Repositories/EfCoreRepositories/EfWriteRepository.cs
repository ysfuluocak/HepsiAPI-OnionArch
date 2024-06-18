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

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            var state = _dbSet.Entry(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Remove(entity));
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            //entity kontrolu eklenebilir
            await DeleteAsync(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Update(entity));
            return entity;
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {

            await Task.Run(() => _dbSet.RemoveRange(entities));
        }
    }
}

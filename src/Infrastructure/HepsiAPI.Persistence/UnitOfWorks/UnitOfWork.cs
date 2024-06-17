using HepsiAPI.Application.Interfaces.Repositories;
using HepsiAPI.Application.Interfaces.UnitOfWorks;
using HepsiAPI.Domain.Common;
using HepsiAPI.Persistence.Context;
using HepsiAPI.Persistence.Repositories.EfCoreRepositories;

namespace HepsiAPI.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public int Save() => _context.SaveChanges();
        //{
        //    return _context.SaveChanges();
        //}

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        //{
        //    return await _context.SaveChangesAsync();
        //}

        public IReadRepository<TEntity> GetReadRepository<TEntity>()
             where TEntity : class, IBaseEntity, new()
             => new EfReadRepository<TEntity>(_context);
        //{
        //    return new EfReadRepository<TEntity>(_context);
        //}

        public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
            where TEntity : class, IBaseEntity, new()
            => new EfWriteRepository<TEntity>(_context);
        //{
        //    return new EfWriteRepository<TEntity>(_context);
    }
}


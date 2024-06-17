using HepsiAPI.Application.Interfaces.Repositories;
using HepsiAPI.Domain.Common;

namespace HepsiAPI.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IWriteRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : class, IBaseEntity, new();
        IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class, IBaseEntity, new();
        Task<int> SaveAsync();
        int Save();

    }
}

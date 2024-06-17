using HepsiAPI.Domain.Common;


namespace HepsiAPI.Application.Interfaces.Repositories
{
    public interface IWriteRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
        Task<int> AddAsync(TEntity entity);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(int id);
    }
}

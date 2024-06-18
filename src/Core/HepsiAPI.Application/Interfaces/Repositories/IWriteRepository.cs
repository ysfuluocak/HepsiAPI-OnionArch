using HepsiAPI.Domain.Common;


namespace HepsiAPI.Application.Interfaces.Repositories
{
    public interface IWriteRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteByIdAsync(int id);
    }
}

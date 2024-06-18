using HepsiAPI.Domain.Entities;

namespace HepsiAPI.Application.Interfaces.Repositories.ProductRepositories
{
    public interface IProductReadRepository : IReadRepository<Product>
    {

        Task<IEnumerable<Product>> GetAllProductWithDetail();
        Task<Product> GetProductByIdWithDetail(int id);
    }
}

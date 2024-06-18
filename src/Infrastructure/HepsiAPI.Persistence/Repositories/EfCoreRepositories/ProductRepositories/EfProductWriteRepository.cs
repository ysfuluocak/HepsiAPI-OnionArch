using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Persistence.Context;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories.ProductRepositories
{
    public class EfProductWriteRepository : EfWriteRepository<Product>, IProductWriteRepository
    {
        public EfProductWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}

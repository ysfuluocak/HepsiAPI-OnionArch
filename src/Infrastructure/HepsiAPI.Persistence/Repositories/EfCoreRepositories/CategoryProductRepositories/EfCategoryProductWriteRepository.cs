using HepsiAPI.Application.Interfaces.Repositories.CategoryProductRepositories;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Persistence.Context;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories.CategoryProductRepositories
{
    public class EfCategoryProductWriteRepository : EfWriteRepository<CategoryProduct>, ICategoryProductWriteRepository
    {
        public EfCategoryProductWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}

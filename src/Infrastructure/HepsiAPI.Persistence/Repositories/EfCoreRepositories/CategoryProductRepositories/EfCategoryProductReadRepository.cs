using HepsiAPI.Application.Interfaces.Repositories.CategoryProductRepositories;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Persistence.Context;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories.CategoryProductRepositories
{
    public class EfCategoryProductReadRepository : EfReadRepository<CategoryProduct>, ICategoryProductReadRepository
    {
        public EfCategoryProductReadRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}

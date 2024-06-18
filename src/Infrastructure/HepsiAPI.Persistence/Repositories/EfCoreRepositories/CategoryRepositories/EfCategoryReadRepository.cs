using HepsiAPI.Application.Interfaces.Repositories.CategoryRepositories;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Persistence.Context;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories.CategoryRepositories
{
    public class EfCategoryReadRepository : EfReadRepository<Category>, ICategoryReadRepository
    {
        public EfCategoryReadRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}

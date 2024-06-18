using HepsiAPI.Application.Interfaces.Repositories.CategoryRepositories;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Persistence.Context;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories.CategoryRepositories
{
    public class EfCategoryWriteRepository : EfWriteRepository<Category>, ICategoryWriteRepository
    {
        public EfCategoryWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using HepsiAPI.Application.Interfaces.Repositories.BrandRepositories;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Persistence.Context;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories.BrandRepositories
{
    public class EfBrandReadRepository : EfReadRepository<Brand>, IBrandReadRepository
    {
        public EfBrandReadRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}

using HepsiAPI.Application.Interfaces.Repositories.BrandRepositories;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Persistence.Context;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories.BrandRepositories
{
    public class EfBrandWriteRepository : EfWriteRepository<Brand>, IBrandWriteRepository
    {
        public EfBrandWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}

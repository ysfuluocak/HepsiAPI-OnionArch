using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using HepsiAPI.Domain.Entities;
using HepsiAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HepsiAPI.Persistence.Repositories.EfCoreRepositories.ProductRepositories
{
    public class EfProductReadRepository : EfReadRepository<Product>, IProductReadRepository
    {
        private readonly AppDbContext _context;
        public EfProductReadRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductWithDetail()
        {
            return await _context.Set<Product>().Include(p => p.Brand).Include(p => p.CategoryProducts).ThenInclude(c => c.Category).ToListAsync();
        }

        public async Task<Product> GetProductByIdWithDetail(int id)
        {
            return await _context.Set<Product>().Include(p => p.Brand).Include(p => p.CategoryProducts).ThenInclude(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}

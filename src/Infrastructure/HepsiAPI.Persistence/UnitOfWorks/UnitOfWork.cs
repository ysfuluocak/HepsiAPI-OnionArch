using HepsiAPI.Application.Interfaces.Repositories.BrandRepositories;
using HepsiAPI.Application.Interfaces.Repositories.CategoryProductRepositories;
using HepsiAPI.Application.Interfaces.Repositories.CategoryRepositories;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using HepsiAPI.Application.Interfaces.UnitOfWorks;
using HepsiAPI.Persistence.Context;

namespace HepsiAPI.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool _disposed = false;

        public IProductWriteRepository GetProductWriteRepository { get; }
        public IProductReadRepository GetProductReadRepository { get; }
        public ICategoryReadRepository GetCategoryReadRepository { get; }
        public ICategoryWriteRepository GetCategoryWriteRepository { get; }
        public IBrandReadRepository GetBrandReadRepository { get; }
        public IBrandWriteRepository GetBrandWriteRepository { get; }
        public ICategoryProductReadRepository GetCategoryProductReadRepository { get; }
        public ICategoryProductWriteRepository GetCategoryProductWriteRepository { get; }

        public UnitOfWork(
            AppDbContext context,
            IProductWriteRepository getProductWriteRepository,
            IProductReadRepository getProductReadRepository,
            ICategoryReadRepository getCategoryReadRepository,
            ICategoryWriteRepository getCategoryWriteRepository,
            IBrandReadRepository getBrandReadRepository,
            IBrandWriteRepository getBrandWriteRepository,
            ICategoryProductReadRepository getCategoryProductReadRepository,
            ICategoryProductWriteRepository getCategoryProductWriteRepository)
        {
            GetProductWriteRepository = getProductWriteRepository;
            GetProductReadRepository = getProductReadRepository;
            GetCategoryReadRepository = getCategoryReadRepository;
            GetCategoryWriteRepository = getCategoryWriteRepository;
            GetBrandReadRepository = getBrandReadRepository;
            GetBrandWriteRepository = getBrandWriteRepository;
            GetCategoryProductReadRepository = getCategoryProductReadRepository;
            GetCategoryProductWriteRepository = getCategoryProductWriteRepository;
            _context = context;
        }



        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _context.DisposeAsync();
                _disposed = true;
            }
        }



        public int Save() => _context.SaveChanges();
        //{
        //    return _context.SaveChanges();
        //}

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        //{
        //    return await _context.SaveChangesAsync();
        //}
    }
}


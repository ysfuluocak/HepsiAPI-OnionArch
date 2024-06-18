using HepsiAPI.Application.Interfaces.Repositories.BrandRepositories;
using HepsiAPI.Application.Interfaces.Repositories.CategoryProductRepositories;
using HepsiAPI.Application.Interfaces.Repositories.CategoryRepositories;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;


namespace HepsiAPI.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IProductWriteRepository GetProductWriteRepository { get; }
        IProductReadRepository GetProductReadRepository { get; }

        ICategoryReadRepository GetCategoryReadRepository { get; }
        ICategoryWriteRepository GetCategoryWriteRepository { get; }

        IBrandReadRepository GetBrandReadRepository { get; }
        IBrandWriteRepository GetBrandWriteRepository { get; }

        ICategoryProductReadRepository GetCategoryProductReadRepository { get; }
        ICategoryProductWriteRepository GetCategoryProductWriteRepository { get; }

        Task<int> SaveAsync();
        int Save();

    }
}

using HepsiAPI.Application.Interfaces.Repositories.BrandRepositories;
using HepsiAPI.Application.Interfaces.Repositories.CategoryProductRepositories;
using HepsiAPI.Application.Interfaces.Repositories.CategoryRepositories;
using HepsiAPI.Application.Interfaces.Repositories.ProductRepositories;
using HepsiAPI.Persistence.Context;
using HepsiAPI.Persistence.Repositories.EfCoreRepositories.BrandRepositories;
using HepsiAPI.Persistence.Repositories.EfCoreRepositories.CategoryProductRepositories;
using HepsiAPI.Persistence.Repositories.EfCoreRepositories.CategoryRepositories;
using HepsiAPI.Persistence.Repositories.EfCoreRepositories.ProductRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HepsiAPI.Persistence
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICategoryReadRepository, EfCategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, EfCategoryWriteRepository>();

            services.AddScoped<IBrandReadRepository, EfBrandReadRepository>();
            services.AddScoped<IBrandWriteRepository, EfBrandWriteRepository>();

            services.AddScoped<IProductReadRepository, EfProductReadRepository>();
            services.AddScoped<IProductWriteRepository, EfProductWriteRepository>();

            services.AddScoped<ICategoryProductReadRepository, EfCategoryProductReadRepository>();
            services.AddScoped<ICategoryProductWriteRepository, EfCategoryProductWriteRepository>();

            return services;
        }

    }
}

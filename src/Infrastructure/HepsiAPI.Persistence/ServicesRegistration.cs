using HepsiAPI.Application.Interfaces.UnitOfWorks;
using HepsiAPI.Persistence.Context;
using HepsiAPI.Persistence.UnitOfWorks;
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }

    }
}

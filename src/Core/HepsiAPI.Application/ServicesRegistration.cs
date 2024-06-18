using HepsiAPI.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace HepsiAPI.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(opt => opt.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}

using FluentValidation;
using HepsiAPI.Application.Behaviors;
using HepsiAPI.Application.Features.ProductFeatures.Commands.CreateProduct;
using HepsiAPI.Application.Mapping;
using HepsiAPI.Application.Rules;
using MediatR;
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


            services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));


            return services;
        }


        public static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly, Type type)
        {
            IEnumerable<Type> types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && t != type);

            foreach (var addedType in types)
            {
                services.AddScoped(addedType);
            }

            return services;
        }


    }
}

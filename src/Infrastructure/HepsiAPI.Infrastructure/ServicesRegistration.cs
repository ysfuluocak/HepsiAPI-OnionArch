using HepsiAPI.Application.Services.Tokens;
using HepsiAPI.Infrastructure.Tokens.Encryption;
using HepsiAPI.Infrastructure.Tokens.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;



namespace HepsiAPI.Infrastructure
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));

            services.AddScoped<ITokenService, JwtManager>();

            TokenOptions? tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                };
            });
            return services;
        }
    }
}

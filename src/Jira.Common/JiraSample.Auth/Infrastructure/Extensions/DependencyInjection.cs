using JiraSample.Auth.Application.Common.Contracts.Authentication;
using JiraSample.Auth.Application.Common.Contracts.Persistance;
using JiraSample.Auth.Infrastructure.Authentication.Config;
using JiraSample.Auth.Infrastructure.Authentication;
using JiraSample.Auth.Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JiraSample.Auth.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddAInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistance(configuration);

        return services;
    }

    private static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}


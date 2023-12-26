using Confluent.Kafka;
using JiraSample.Query.Application.Contracts.Consumers;
using JiraSample.Query.Application.Contracts.EventHandlers;
using JiraSample.Query.Application.Contracts.Repositories;
using JiraSample.Query.Infrastructure.HostedServices;
using JiraSample.Query.Infrastructure.Persistance;
using JiraSample.Query.Infrastructure.Services.Consumers;
using JiraSample.Query.Infrastructure.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EventHandler = JiraSample.Query.Infrastructure.Services.EventHandlers.EventHandler;

namespace JiraSample.Query.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAInfrastructure(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddDbContext<JiraDatabaseContext>(
            m => m.UseSqlServer(configuration.GetConnectionString("JiraDatabaseConnection")), ServiceLifetime.Singleton);

        services.AddHostedService<ConsumerHostedService>();

        services.AddScoped<IEventConsumer, EventConsumer>();
        services.AddScoped<IEventHandler, EventHandler>();
        services.AddScoped<IJiraItemRepository, JiraItemRepository>();
        services.AddScoped<IJiraItemHistoryRepository, JiraItemHistoryRepository>();

        return services;
    }
}
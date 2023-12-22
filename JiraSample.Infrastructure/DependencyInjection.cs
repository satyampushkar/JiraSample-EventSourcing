using Confluent.Kafka;
using JiraSample.Application.Contracts.EventSourcing;
using JiraSample.Application.Contracts.Publishers;
using JiraSample.Application.Contracts.Repositories;
using JiraSample.Domain.JiraItem;
using JiraSample.Infrastructure.Configurations;
using JiraSample.Infrastructure.Services.EventSourcing;
using JiraSample.Infrastructure.Services.Publishers;
using JiraSample.Infrastructure.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JiraSample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {   
        //services.Configure<MongoDbConfig>(options => configuration.GetSection(nameof(MongoDbConfig)));
        //services.Configure<ProducerConfig>(options => configuration.GetSection(nameof(ProducerConfig)));


        services.AddScoped<IEventStoreRepository, EventStoreRepository>();
        services.AddScoped<IEventPublisher, EventPublisher>();
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<IEventSourcingHandler<JiraItemAggregate>, EventSourcingHandler>();

        return services;
    }
}

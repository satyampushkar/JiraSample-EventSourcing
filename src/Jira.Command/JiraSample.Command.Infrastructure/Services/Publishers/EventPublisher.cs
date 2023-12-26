using Confluent.Kafka;
using JiraSample.Application.Contracts.Publishers;
using JiraSample.Common.Events;
using JiraSample.Infrastructure.Services.Publishers.Exceptions;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace JiraSample.Infrastructure.Services.Publishers;

public class EventPublisher : IEventPublisher
{
    private readonly ProducerConfig _config;

    public EventPublisher(IOptions<ProducerConfig> config)
    {
        _config = config.Value;
    }

    public async Task PublishAsync<T>(string topic, T @event) where T : BaseEvent
    {
        using var producer = new ProducerBuilder<string, string>(_config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .Build();

        var eventMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(@event, @event.GetType())
        };

        var deliveryResult = await producer.ProduceAsync(topic, eventMessage);

        if (deliveryResult.Status == PersistenceStatus.NotPersisted)
        {
            throw new EventNotPublishedException($"Could not produce {@event.GetType().Name} message to topic - {topic} due to the following reason: {deliveryResult.Message}.");
        }
    }
}

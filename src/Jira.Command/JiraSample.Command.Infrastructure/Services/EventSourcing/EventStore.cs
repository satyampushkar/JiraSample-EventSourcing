using JiraSample.Application.Contracts.EventSourcing;
using JiraSample.Application.Contracts.Publishers;
using JiraSample.Application.Contracts.Repositories;
using JiraSample.Common.Events;
using JiraSample.Domain.JiraItem;
using JiraSample.Infrastructure.Services.EventSourcing.Exceptions;

namespace JiraSample.Infrastructure.Services.EventSourcing;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventPublisher _eventPublisher;

    public EventStore(IEventStoreRepository eventStoreRepository, IEventPublisher eventPublisher)
    {
        _eventStoreRepository = eventStoreRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if (eventStream == null || !eventStream.Any())
            throw new AggregateNotFoundException("Incorrect Jira ID provided!");

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException();

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel
            {
                TimeStamp = DateTime.Now,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(JiraItemAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };

            await _eventStoreRepository.SaveAsync(eventModel);

            var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
            await _eventPublisher.PublishAsync(topic, @event);
        }
    }
}

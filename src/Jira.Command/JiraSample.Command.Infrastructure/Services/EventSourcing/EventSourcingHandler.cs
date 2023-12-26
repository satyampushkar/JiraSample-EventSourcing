using JiraSample.Application.Contracts.EventSourcing;
using JiraSample.Domain.JiraItem;
using JiraSample.Domain.SeedWork;

namespace JiraSample.Infrastructure.Services.EventSourcing;

public class EventSourcingHandler : IEventSourcingHandler<JiraItemAggregate>
{
    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<JiraItemAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new JiraItemAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);

        if (events == null || !events.Any()) return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;
    }

    //TODO: Convert this to generic
    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
        aggregate.MarkChangesAsCommitted();
    }
}

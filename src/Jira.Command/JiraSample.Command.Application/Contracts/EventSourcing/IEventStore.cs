using JiraSample.Common.Events;

namespace JiraSample.Application.Contracts.EventSourcing;

public interface IEventStore
{
    Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);
}

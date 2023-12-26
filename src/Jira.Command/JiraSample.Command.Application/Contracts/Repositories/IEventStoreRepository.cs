using JiraSample.Common.Events;

namespace JiraSample.Application.Contracts.Repositories;

public interface IEventStoreRepository
{
    Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
    Task SaveAsync(EventModel @event);
}

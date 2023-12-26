using JiraSample.Domain.JiraItem;
using JiraSample.Domain.SeedWork;

namespace JiraSample.Application.Contracts.EventSourcing;

public interface IEventSourcingHandler<T>
{
    Task<JiraItemAggregate> GetByIdAsync(Guid aggregateId);
    Task SaveAsync(AggregateRoot aggregate);
}

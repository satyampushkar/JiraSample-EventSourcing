using JiraSample.Common.Events;

namespace JiraSample.Application.Contracts.Publishers;

public interface IEventPublisher
{
    Task PublishAsync<T>(string topic, T @event) where T : BaseEvent;
}

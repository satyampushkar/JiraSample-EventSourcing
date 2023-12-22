using JiraSample.Common.Events;

namespace JiraSample.Query.Application.Contracts.EventHandlers;

public interface IEventHandler
{
    Task On(JiraItemCreatedEvent @event);
    Task On(JiraItemUpdatedEvent @event);
}

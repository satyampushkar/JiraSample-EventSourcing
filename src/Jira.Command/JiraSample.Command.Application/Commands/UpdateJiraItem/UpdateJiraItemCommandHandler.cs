using JiraSample.Application.Contracts.EventSourcing;
using JiraSample.Domain.JiraItem;
using MediatR;

namespace JiraSample.Application.Commands.UpdateJiraItem;

public class UpdateJiraItemCommandHandler : IRequestHandler<UpdateJiraItemCommand, bool>
{
    private readonly IEventSourcingHandler<JiraItemAggregate> _eventSourcingHandler;

    public UpdateJiraItemCommandHandler(IEventSourcingHandler<JiraItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<bool> Handle(UpdateJiraItemCommand command, CancellationToken cancellationToken)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.id);
        aggregate.UpdateJiraItem(command.name,
                                 command.description,
                                 command.ItemType,
                                 command.Status,
                                 command.Asignee,
                                 command.ParentId);

        await _eventSourcingHandler.SaveAsync(aggregate);
        return true;
    }
}

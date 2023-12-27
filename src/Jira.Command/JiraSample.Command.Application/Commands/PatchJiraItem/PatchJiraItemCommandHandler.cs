using JiraSample.Application.Contracts.EventSourcing;
using JiraSample.Domain.JiraItem;
using JiraSample.Domain.JiraItem.Enums;
using MediatR;
using Microsoft.AspNetCore.JsonPatch.Operations;
using MongoDB.Bson.Serialization.Serializers;

namespace JiraSample.Command.Application.Commands.PatchJiraItem;

public class PatchJiraItemCommandHandler : IRequestHandler<PatchJiraItemCommand, bool>
{
    private readonly IEventSourcingHandler<JiraItemAggregate> _eventSourcingHandler;

    public PatchJiraItemCommandHandler(IEventSourcingHandler<JiraItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<bool> Handle(PatchJiraItemCommand command, CancellationToken cancellationToken)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.id);

        foreach (Operation? operation in command.JsonPatchDocument.Operations)
        {
            if (operation.OperationType != OperationType.Add
                && operation.OperationType != OperationType.Replace
                && operation.OperationType != OperationType.Remove)
            {
                throw new InvalidOperationException();
            }

            if (operation.path.Equals(nameof(aggregate.Name), StringComparison.CurrentCultureIgnoreCase))
            {
                aggregate.UpdateJiraItemName(operation.value.ToString());
            }
            else if (operation.path.Equals(nameof(aggregate.Description), StringComparison.CurrentCultureIgnoreCase))
            {
                aggregate.UpdateJiraItemDescription(operation.value.ToString());
            }
            else if (operation.path.Equals(nameof(aggregate.ItemType), StringComparison.CurrentCultureIgnoreCase))
            {
                aggregate.UpdateJiraItemType(JiraItemType.FromName(operation.value.ToString()));
            }
            else if (operation.path.Equals(nameof(aggregate.ItemStatus), StringComparison.CurrentCultureIgnoreCase))
            {
                aggregate.UpdateJiraItemStatus(JiraItemStatus.FromName(operation.value.ToString()));
            }
            else if (operation.path.Equals(nameof(aggregate.Asignee), StringComparison.CurrentCultureIgnoreCase))
            {
                aggregate.UpdateJiraItemAsignee(operation.value.ToString());
            }
            else if (operation.path.Equals(nameof(aggregate.ParentId), StringComparison.CurrentCultureIgnoreCase))
            {
                aggregate.UpdateJiraItemParent(new Guid(operation.value.ToString()));
            }
            else if (operation.path.Equals(nameof(aggregate.Children), StringComparison.CurrentCultureIgnoreCase))
            {
                aggregate.UpdateJiraItemChildren((operation.value  as List<Guid>));
            }
        }

        await _eventSourcingHandler.SaveAsync(aggregate);
        return true;
    }
}

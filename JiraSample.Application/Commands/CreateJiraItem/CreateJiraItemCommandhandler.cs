using JiraSample.Application.Contracts.EventSourcing;
using JiraSample.Domain.JiraItem;
using MediatR;

namespace JiraSample.Application.Commands.CreateJiraItem;

public class CreateJiraItemCommandhandler : IRequestHandler<CreateJiraItemCommand, JiraItemAggregate>
{
    private readonly IEventSourcingHandler<JiraItemAggregate> _eventSourcingHandler;

    public CreateJiraItemCommandhandler(IEventSourcingHandler<JiraItemAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task<JiraItemAggregate> Handle(CreateJiraItemCommand command, CancellationToken cancellationToken)
    {
        var newJiraItemAggregate = new JiraItemAggregate(command.name,
                                                         command.description,
                                                         command.ItemType,
                                                         command.Author,
                                                         command.Asignee,
                                                         command.ParentId);

        await _eventSourcingHandler.SaveAsync(newJiraItemAggregate);

        return newJiraItemAggregate;
    }
}

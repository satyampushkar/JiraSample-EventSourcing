using JiraSample.Domain.JiraItem;
using JiraSample.Domain.JiraItem.Enums;
using MediatR;

namespace JiraSample.Application.Commands.CreateJiraItem;

public record CreateJiraItemCommand(
    string name,
    string description,
    JiraItemType ItemType,
    string Author,
    string Asignee,
    Guid ParentId) : IRequest<JiraItemAggregate>;

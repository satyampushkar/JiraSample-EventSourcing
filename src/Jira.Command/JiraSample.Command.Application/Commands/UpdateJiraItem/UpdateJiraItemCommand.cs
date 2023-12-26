using JiraSample.Domain.JiraItem.Enums;
using MediatR;

namespace JiraSample.Application.Commands.UpdateJiraItem;

public record UpdateJiraItemCommand(
    Guid id,
    string name,
    string description,
    JiraItemType ItemType,
    JiraItemStatus Status,
    string Asignee,
    Guid ParentId) : IRequest<bool>;


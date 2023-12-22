using JiraSample.Domain.JiraItem.Enums;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace JiraSample.Command.Application.Commands.PatchJiraItem;

public record PatchJiraItemCommand(
    Guid id,
    //string name,
    //string description,
    //JiraItemType ItemType,
    //JiraItemStatus Status,
    //string Asignee,
    //Guid ParentId,
    //List<Guid> Children,
    JsonPatchDocument JsonPatchDocument) : IRequest<bool>;
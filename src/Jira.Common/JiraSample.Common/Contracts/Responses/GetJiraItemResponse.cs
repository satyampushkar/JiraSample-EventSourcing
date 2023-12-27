namespace JiraSample.Common.Contracts.Responses;

public record GetJiraItemResponse(
    Guid Id,
    string Name,
    string Description,
    string ItemType,
    string ItemStatus,
    string Asignee,
    string Author,
    string ParentId,
    DateTime CreatedDateTime,
    DateTime LastUpdatedDateTime);
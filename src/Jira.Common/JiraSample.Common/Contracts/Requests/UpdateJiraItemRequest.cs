using JiraSample.Common.Enums;

namespace JiraSample.Common.Contracts.Requests;

public record UpdateJiraItemRequest(
    string Name,
    string Description,
    string ItemType,
    string ItemStatus,
    string Asignee,
    Guid ParentId);
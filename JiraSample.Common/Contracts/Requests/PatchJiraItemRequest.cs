using JiraSample.Common.Enums;

namespace JiraSample.Common.Contracts.Requests;

public record PatchJiraItemRequest(string Name,
    string Description,
    string ItemType,
    string ItemStatus,
    string Asignee,
    Guid ParentId,
    List<Guid> Children);
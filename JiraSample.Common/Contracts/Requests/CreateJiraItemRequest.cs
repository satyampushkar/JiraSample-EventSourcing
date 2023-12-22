using JiraSample.Common.Enums;

namespace JiraSample.Common.Contracts.Requests;

public record CreateJiraItemRequest(
    string Name,
    string Description,
    string ItemType,
    string Author,
    string Asignee,
    Guid ParentId);
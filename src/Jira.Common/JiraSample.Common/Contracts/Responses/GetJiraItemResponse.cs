namespace JiraSample.Common.Contracts.Responses;

public record GetJiraItemResponse(
    Guid Id,
    string Name,
    string Description);
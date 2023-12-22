namespace JiraSample.Common.Contracts.Responses;

public record CreateJiraItemResponse(
    string id,
    string name,
    string description);

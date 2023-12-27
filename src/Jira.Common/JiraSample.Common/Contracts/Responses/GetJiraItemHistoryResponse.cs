﻿namespace JiraSample.Common.Contracts.Responses;

public record GetJiraItemHistoryResponse(List<JiraItemHistoryResponse> JiraItemHistoryResponses);

public record JiraItemHistoryResponse(
    Guid Id,
    string ActionPerformed,
    string ChangedValue,
    DateTime ActionPerformedAt);

public enum ActionPerformed
{
    Create,
    Update,
    Delete
}
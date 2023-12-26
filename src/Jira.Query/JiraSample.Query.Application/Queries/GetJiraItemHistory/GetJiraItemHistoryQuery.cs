using JiraSample.Query.Domain.Entities;
using MediatR;

namespace JiraSample.Query.Application.Queries.GetJiraItemHistory;

public record GetJiraItemHistoryQuery(Guid JiraItemId) : IRequest<List<JiraItemHistoryEntity>>;

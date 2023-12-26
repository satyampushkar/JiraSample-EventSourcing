using JiraSample.Query.Application.Contracts.Repositories;
using JiraSample.Query.Domain.Entities;
using MediatR;

namespace JiraSample.Query.Application.Queries.GetJiraItemHistory;

public class GetJiraItemHistoryQueryHandler : IRequestHandler<GetJiraItemHistoryQuery, List<JiraItemHistoryEntity>>
{
    private readonly IJiraItemHistoryRepository _jiraItemHistoryRepository;

    public GetJiraItemHistoryQueryHandler(IJiraItemHistoryRepository jiraItemHistoryRepository)
    {
        _jiraItemHistoryRepository = jiraItemHistoryRepository;
    }

    public async Task<List<JiraItemHistoryEntity>> Handle(GetJiraItemHistoryQuery query, CancellationToken cancellationToken)
    {
        return await _jiraItemHistoryRepository.GetAllByIdAsync(query.JiraItemId);
    }
}

using JiraSample.Query.Application.Contracts.Repositories;
using JiraSample.Query.Domain.Entities;
using MediatR;

namespace JiraSample.Query.Application.Queries.GetJiraItems;

public class GetJiraItemsQueryHandler : IRequestHandler<GetJiraItemsQuery, List<JiraItemEntity>>
{
    private readonly IJiraItemRepository _jiraItemRepository;

    public GetJiraItemsQueryHandler(IJiraItemRepository jiraItemRepository)
    {
        _jiraItemRepository = jiraItemRepository;
    }
    public async Task<List<JiraItemEntity>> Handle(GetJiraItemsQuery request, CancellationToken cancellationToken)
    {
        return await _jiraItemRepository.ListAllAsync();
    }
}

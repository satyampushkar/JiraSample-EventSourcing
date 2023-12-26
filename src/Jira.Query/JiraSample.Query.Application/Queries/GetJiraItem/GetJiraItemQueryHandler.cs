using JiraSample.Query.Application.Contracts.Repositories;
using JiraSample.Query.Domain.Entities;
using MediatR;

namespace JiraSample.Query.Application.Queries.GetJiraItem;

public class GetJiraItemQueryHandler : IRequestHandler<GetJiraItemQuery, JiraItemEntity>
{
    private readonly IJiraItemRepository _jiraItemRepository;

    public GetJiraItemQueryHandler(IJiraItemRepository jiraItemRepository)
    {
        _jiraItemRepository = jiraItemRepository;
    }

    public async Task<JiraItemEntity> Handle(GetJiraItemQuery query, CancellationToken cancellationToken)
    {
        return await _jiraItemRepository.GetByIdAsync(query.Id);
    }
}

using JiraSample.Query.Domain.Entities;

namespace JiraSample.Query.Application.Contracts.Repositories;

public interface IJiraItemHistoryRepository
{
    Task CreateAsync(JiraItemHistoryEntity jiraItemHistory);
    Task<List<JiraItemHistoryEntity>> GetAllByIdAsync(Guid jiraItemId);
}

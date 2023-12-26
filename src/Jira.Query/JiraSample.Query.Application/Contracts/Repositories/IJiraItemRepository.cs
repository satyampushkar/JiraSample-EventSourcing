using JiraSample.Query.Domain.Entities;

namespace JiraSample.Query.Application.Contracts.Repositories;

public interface IJiraItemRepository
{
    Task CreateAsync(JiraItemEntity jiraItem);
    Task UpdateAsync(JiraItemEntity jiraItem);
    Task DeleteAsync(Guid jiraItemId);
    Task<JiraItemEntity> GetByIdAsync(Guid jiraItemId);
    Task<List<JiraItemEntity>> ListAllAsync();
}

using JiraSample.Query.Application.Contracts.Repositories;
using JiraSample.Query.Domain.Entities;
using JiraSample.Query.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace JiraSample.Query.Infrastructure.Services.Repositories;

public class JiraItemHistoryRepository : IJiraItemHistoryRepository
{
    private readonly JiraDatabaseContext _jiraDatabaseContext;

    public JiraItemHistoryRepository(JiraDatabaseContext jiraDatabaseContext)
    {
        _jiraDatabaseContext = jiraDatabaseContext;
    }
    public async Task CreateAsync(JiraItemHistoryEntity jiraItemHistory)
    {
        _jiraDatabaseContext.JiraItemsHistory.Add(jiraItemHistory);
        await _jiraDatabaseContext.SaveChangesAsync();
    }

    public async Task<List<JiraItemHistoryEntity>> GetAllByIdAsync(Guid jiraItemId)
    {
        return await _jiraDatabaseContext.JiraItemsHistory
            .AsNoTracking()
            .Where(x => x.JiraItemId == jiraItemId)
            .ToListAsync();
    }
}

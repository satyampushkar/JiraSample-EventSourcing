using JiraSample.Query.Application.Contracts.Repositories;
using JiraSample.Query.Domain.Entities;
using JiraSample.Query.Domain.Entities.Enums;
using JiraSample.Query.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace JiraSample.Query.Infrastructure.Services.Repositories;

public class JiraItemRepository : IJiraItemRepository
{
    private readonly JiraDatabaseContext _jiraDatabaseContext;

    public JiraItemRepository(JiraDatabaseContext jiraDatabaseContext)
    {
        _jiraDatabaseContext = jiraDatabaseContext;
    }

    public async Task CreateAsync(JiraItemEntity jiraItem)
    {
        try
        {
            _jiraDatabaseContext.JiraItems.Add(jiraItem);
            await _jiraDatabaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {

            throw;
        }
        
    }

    public async Task DeleteAsync(Guid jiraItemId)
    {
        var jiraItem = await GetByIdAsync(jiraItemId);

        if (jiraItem == null) return;

        _jiraDatabaseContext.JiraItems.Remove(jiraItem);
    }

    public async Task<JiraItemEntity> GetByIdAsync(Guid jiraItemId)
    {
        return await _jiraDatabaseContext.JiraItems.FirstOrDefaultAsync(item => item.Id == jiraItemId);
    }

    public async Task<List<JiraItemEntity>> ListAllAsync()
    {
        return _jiraDatabaseContext.JiraItems.AsNoTracking().ToList();
    }

    public async Task UpdateAsync(JiraItemEntity jiraItem)
    {
        var item = await GetByIdAsync(jiraItem.Id);

        item.Name = jiraItem.Name ?? item.Name;
        item.Description = jiraItem.Description ?? item.Description;
        item.Asignee = jiraItem.Asignee ?? item.Asignee;
        item.ItemType = jiraItem.ItemType == JiraItemType.None ? item.ItemType : jiraItem.ItemType;
        item.ItemStatus = jiraItem.ItemStatus == JiraItemStatus.None ? item.ItemStatus : jiraItem.ItemStatus;
        item.ParentId = jiraItem.ParentId ?? item.ParentId;
        item.UpdatedDateTime = jiraItem.UpdatedDateTime;

        _jiraDatabaseContext.JiraItems.Update(item);

        //_jiraDatabaseContext.JiraItems.Entry(jiraItem).State = EntityState.Modified;

        await _jiraDatabaseContext.SaveChangesAsync();

    }
}

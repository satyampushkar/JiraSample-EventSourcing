using JiraSample.Common.Events;
using JiraSample.Query.Application.Contracts.EventHandlers;
using JiraSample.Query.Application.Contracts.Repositories;
using JiraSample.Query.Domain.Entities;
using JiraSample.Query.Domain.Entities.Enums;

namespace JiraSample.Query.Infrastructure.Services.EventHandlers;

public class EventHandler : IEventHandler
{
    private readonly IJiraItemRepository _jiraItemRepository;
    private readonly IJiraItemHistoryRepository _jiraItemHistoryRepository;

    public EventHandler(IJiraItemRepository jiraItemRepository, IJiraItemHistoryRepository jiraItemHistoryRepository)
    {
        _jiraItemRepository = jiraItemRepository;
        _jiraItemHistoryRepository = jiraItemHistoryRepository;
    }

    public async Task On(JiraItemCreatedEvent @event)
    {
        JiraItemEntity jiraItemEntity = new()
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            ItemType = (JiraItemType)Enum.Parse(typeof(JiraItemType), @event.ItemType.Name),
            Author = @event.Author,
            Asignee = @event.Asignee ?? string.Empty,
            ParentId = @event.ParentId ?? Guid.Empty,
            Status = (JiraItemStatus)Enum.Parse(typeof(JiraItemStatus), @event.Status.Name),
            CreatedDateTime = @event.CreatedDateTime,
            UpdatedDateTime = @event.CreatedDateTime,
        };
        await _jiraItemRepository.CreateAsync(jiraItemEntity);

        await UpdateHistory(@event.Id, @event.Type, @event.CreatedDateTime);
    }

    public async Task On(JiraItemUpdatedEvent @event)
    {
        JiraItemEntity jiraItemEntity = new()
        {
            Id = @event.Id,
            Name = @event.Name,
            Description = @event.Description,
            ItemType = (JiraItemType)Enum.Parse(typeof(JiraItemType), @event.ItemType.Name),
            Asignee = @event.Asignee,
            ParentId = @event.ParentId,
            Status = (JiraItemStatus)Enum.Parse(typeof(JiraItemStatus), @event.Status.Name),
            UpdatedDateTime = @event.UpdatedDateTime,
        };
        await _jiraItemRepository.UpdateAsync(jiraItemEntity);

        await UpdateHistory(@event.Id, @event.Type,  @event.UpdatedDateTime, jiraItemEntity.ToString());
    }

    public async Task On(JiraItemNameUpdatedEvent @event)
    {
        JiraItemEntity jiraItemEntity = new()
        {
            Id = @event.Id,
            Name = @event.Name,
            UpdatedDateTime = @event.UpdatedDateTime,
        };
        await _jiraItemRepository.UpdateAsync(jiraItemEntity);

        await UpdateHistory(@event.Id, @event.Type, @event.UpdatedDateTime, @event.Name);
    }

    public async Task On(JiraItemDescriptionUpdatedEvent @event)
    {
        JiraItemEntity jiraItemEntity = new()
        {
            Id = @event.Id,
            Description = @event.Description,
            UpdatedDateTime = @event.UpdatedDateTime,
        };
        await _jiraItemRepository.UpdateAsync(jiraItemEntity);

        await UpdateHistory(@event.Id, @event.Type, @event.UpdatedDateTime, @event.Description);
    }

    public async Task On(JiraItemAsigneeUpdatedEvent @event)
    {
        JiraItemEntity jiraItemEntity = new()
        {
            Id = @event.Id,
            Asignee = @event.Asignee,
            UpdatedDateTime = @event.UpdatedDateTime,
        };
        await _jiraItemRepository.UpdateAsync(jiraItemEntity);

        await UpdateHistory(@event.Id, @event.Type, @event.UpdatedDateTime, @event.Asignee);
    }

    //public async Task On(JiraItemChildrenUpdatedEvent @event)
    //{
    //    JiraItemEntity jiraItemEntity = new()
    //    {
    //        Id = @event.Id,
    //        Children = @event.Children,
    //        UpdatedDateTime = @event.UpdatedDateTime,
    //    };
    //    await _jiraItemRepository.UpdateAsync(jiraItemEntity);

    //    await UpdateHistory(@event.Id, @event.Type, @event.UpdatedDateTime, @event.Children.ToList().ToString());
    //}

    public async Task On(JiraItemParentUpdatedEvent @event)
    {
        JiraItemEntity jiraItemEntity = new()
        {
            Id = @event.Id,
            ParentId = @event.ParentId,
            UpdatedDateTime = @event.UpdatedDateTime,
        };
        await _jiraItemRepository.UpdateAsync(jiraItemEntity);

        await UpdateHistory(@event.Id, @event.Type, @event.UpdatedDateTime, @event.ParentId.ToString());
    }

    public async Task On(JiraItemStatusUpdatedEvent @event)
    {
        JiraItemEntity jiraItemEntity = new()
        {
            Id = @event.Id,
            Status = (JiraItemStatus)Enum.Parse(typeof(JiraItemStatus), @event.Status.Name),
            UpdatedDateTime = @event.UpdatedDateTime,
        };
        await _jiraItemRepository.UpdateAsync(jiraItemEntity);

        await UpdateHistory(@event.Id, @event.Type, @event.UpdatedDateTime, @event.Status.Name);
    }

    public async Task On(JiraItemTypeUpdatedEvent @event)
    {
        JiraItemEntity jiraItemEntity = new()
        {
            Id = @event.Id,
            ItemType = (JiraItemType)Enum.Parse(typeof(JiraItemType), @event.ItemType.Name),
            UpdatedDateTime = @event.UpdatedDateTime,
        };
        await _jiraItemRepository.UpdateAsync(jiraItemEntity);

        await UpdateHistory(@event.Id, @event.Type, @event.UpdatedDateTime, @event.ItemType.Name);
    }


    private async Task UpdateHistory(Guid jiraItemId, string eventName, DateTime actionPerformedAt, string changedValue = null)
    {
        JiraItemHistoryEntity jiraItemHistoryEntity = new()
        {
            JiraItemId = jiraItemId,
            ActionPerformed = eventName.Replace("Event", ""),
            ChangedValue = changedValue,
            ActionPerformedAt = actionPerformedAt,
        };

        await _jiraItemHistoryRepository.CreateAsync(jiraItemHistoryEntity);
    }
}

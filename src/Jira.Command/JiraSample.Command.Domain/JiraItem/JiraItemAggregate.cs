using JiraSample.Command.Domain.JiraItem.Exceptions;
using JiraSample.Common.Enums;
using JiraSample.Common.Events;
using JiraSample.Domain.JiraItem.Enums;
using JiraSample.Domain.SeedWork;
using System.Xml.Linq;

namespace JiraSample.Domain.JiraItem;

public class JiraItemAggregate : AggregateRoot
{
    private readonly List<JiraItemAggregate> _children = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public JiraItemType ItemType { get; private set; }
    public string Author { get; private set; }
    public string Asignee { get; private set; }
    public Guid? ParentId { get; private set; } = null;
    public JiraItemStatus ItemStatus { get; private set; } = JiraItemStatus.ToDo;
    public List<Guid> Children { get; private set; } = new();
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public JiraItemAggregate()
    {
        
    }
    public JiraItemAggregate(string name, string description, JiraItemType itemType, string author, string? assignee = null, Guid? parentId = null)
    {
        //TODO: check for reqd value and throw domain exceptions
        RaiseEvent(new JiraItemCreatedEvent
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ItemType = new ItemType(itemType.Id, itemType.Name),
            Author = author,
            Asignee = assignee, 
            ParentId = parentId,
            Status = Common.Enums.ItemStatus.ToDo,
            CreatedDateTime = DateTime.UtcNow
    });
    }

    public void Apply(JiraItemCreatedEvent jiraItemCreatedEvent)
    {
        _id = jiraItemCreatedEvent.Id;
        Name = jiraItemCreatedEvent.Name;
        Description = jiraItemCreatedEvent.Description;
        ItemType = new JiraItemType(jiraItemCreatedEvent.ItemType.Id, jiraItemCreatedEvent.ItemType.Name);
        Author = jiraItemCreatedEvent.Author;
        ParentId = jiraItemCreatedEvent.ParentId;
        ItemStatus = new JiraItemStatus(jiraItemCreatedEvent.Status.Id, jiraItemCreatedEvent.Status.Name);
        CreatedDateTime = jiraItemCreatedEvent.CreatedDateTime;
    }

    public void UpdateJiraItem(string name, string description, JiraItemType itemType, JiraItemStatus itemStatus, string? assignee = null, Guid? parentId = null)
    {
        //TODO: check for reqd value and throw domain exceptions
        RaiseEvent(new JiraItemUpdatedEvent
        {
            Id = _id,
            Name = name,
            Description = description,
            ItemType = new ItemType(itemType.Id, itemType.Name),
            Asignee = assignee,
            ParentId = parentId,
            Status = new ItemStatus(itemStatus.Id, itemStatus.Name),
            UpdatedDateTime = DateTime.UtcNow
        });
    }

    public void Apply(JiraItemUpdatedEvent jiraItemUpdatedEvent)
    {
        _id = jiraItemUpdatedEvent.Id;
        Name = jiraItemUpdatedEvent.Name;
        Description = jiraItemUpdatedEvent.Description;
        ItemType = new JiraItemType(jiraItemUpdatedEvent.ItemType.Id, jiraItemUpdatedEvent.ItemType.Name);
        Asignee = jiraItemUpdatedEvent.Asignee;
        ParentId = jiraItemUpdatedEvent.ParentId;
        ItemStatus = new JiraItemStatus(jiraItemUpdatedEvent.Status.Id, jiraItemUpdatedEvent.Status.Name);
        UpdatedDateTime = jiraItemUpdatedEvent.UpdatedDateTime;
    }

    public void UpdateJiraItemName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        RaiseEvent(new JiraItemNameUpdatedEvent
        {
            Id = _id,
            Name = name,
            UpdatedDateTime = DateTime.UtcNow
        });
    }

    public void Apply(JiraItemNameUpdatedEvent jiraItemNameUpdatedEvent)
    {
        _id = jiraItemNameUpdatedEvent.Id;
        Name = jiraItemNameUpdatedEvent.Name;
        UpdatedDateTime = jiraItemNameUpdatedEvent.UpdatedDateTime;
    }

    public void UpdateJiraItemDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description));
        }

        RaiseEvent(new JiraItemDescriptionUpdatedEvent
        {
            Id = _id,
            Description = description,
            UpdatedDateTime = DateTime.UtcNow
        });
    }

    public void Apply(JiraItemDescriptionUpdatedEvent jiraItemDescriptionUpdatedEvent)
    {
        _id = jiraItemDescriptionUpdatedEvent.Id;
        Description = jiraItemDescriptionUpdatedEvent.Description;
        UpdatedDateTime = jiraItemDescriptionUpdatedEvent.UpdatedDateTime;
    }

    public void UpdateJiraItemType(JiraItemType itemType)
    {
        if (string.IsNullOrWhiteSpace(itemType.Id.ToString())
            || string.IsNullOrWhiteSpace(itemType.Name))
        {
            throw new ArgumentNullException(nameof(itemType));
        }

        RaiseEvent(new JiraItemTypeUpdatedEvent
        {
            Id = _id,
            ItemType = new ItemType(itemType.Id, itemType.Name),
            UpdatedDateTime = DateTime.UtcNow
        });
    }

    public void Apply(JiraItemTypeUpdatedEvent jiraItemTypeUpdatedEvent)
    {
        _id = jiraItemTypeUpdatedEvent.Id;
        ItemType = new JiraItemType(jiraItemTypeUpdatedEvent.ItemType.Id, jiraItemTypeUpdatedEvent.ItemType.Name);
        UpdatedDateTime = jiraItemTypeUpdatedEvent.UpdatedDateTime;
    }

    public void UpdateJiraItemStatus(JiraItemStatus itemStatus)
    {
        if (string.IsNullOrWhiteSpace(itemStatus.Id.ToString())
            || string.IsNullOrWhiteSpace(itemStatus.Name))
        {
            throw new ArgumentNullException(nameof(itemStatus));
        }

        RaiseEvent(new JiraItemStatusUpdatedEvent
        {
            Id = _id,
            Status = new ItemStatus(itemStatus.Id, itemStatus.Name),
            UpdatedDateTime = DateTime.UtcNow
        });
    }

    public void Apply(JiraItemStatusUpdatedEvent jiraItemStatusUpdatedEvent)
    {
        _id = jiraItemStatusUpdatedEvent.Id;
        ItemStatus = new JiraItemStatus(jiraItemStatusUpdatedEvent.Status.Id, jiraItemStatusUpdatedEvent.Status.Name);
        UpdatedDateTime = jiraItemStatusUpdatedEvent.UpdatedDateTime;
    }

    public void UpdateJiraItemAsignee(string assignee)
    {
        if (string.IsNullOrWhiteSpace(assignee))
        {
            throw new ArgumentNullException(nameof(assignee));
        }

        RaiseEvent(new JiraItemAsigneeUpdatedEvent
        {
            Id = _id,
            Asignee = assignee,
            UpdatedDateTime = DateTime.UtcNow
        });
    }

    public void Apply(JiraItemAsigneeUpdatedEvent jiraItemAsigneeUpdatedEvent)
    {
        _id = jiraItemAsigneeUpdatedEvent.Id;
        Asignee = jiraItemAsigneeUpdatedEvent.Asignee;
        UpdatedDateTime = jiraItemAsigneeUpdatedEvent.UpdatedDateTime;
    }

    public void UpdateJiraItemParent(Guid parentId)
    {
        if (string.IsNullOrWhiteSpace(parentId.ToString()))
        {
            throw new ArgumentNullException(nameof(parentId));
        }

        RaiseEvent(new JiraItemParentUpdatedEvent
        {
            Id = _id,
            ParentId = parentId,
            UpdatedDateTime = DateTime.UtcNow
        });
    }

    public void Apply(JiraItemParentUpdatedEvent jiraItemParentUpdatedEvent)
    {
        _id = jiraItemParentUpdatedEvent.Id;
        ParentId = jiraItemParentUpdatedEvent.ParentId;
        UpdatedDateTime = jiraItemParentUpdatedEvent.UpdatedDateTime;
    }

    public void UpdateJiraItemChildren(List<Guid> children)
    {
        RaiseEvent(new JiraItemChildrenUpdatedEvent
        {
            Id = _id,
            Children = children,
            UpdatedDateTime = DateTime.UtcNow
        });
    }

    public void Apply(JiraItemChildrenUpdatedEvent jiraItemChildrenUpdatedEvent)
    {
        _id = jiraItemChildrenUpdatedEvent.Id;
        Children = jiraItemChildrenUpdatedEvent.Children;
        UpdatedDateTime = jiraItemChildrenUpdatedEvent.UpdatedDateTime;
    }

    //private void CheckIfChildrenUpdationPossible(List<Guid> children)
    //{
    //    if (children is null || !children.Any())
    //    {
    //        throw new ArgumentNullException(nameof(children));
    //    }

    //    foreach (Guid child in children) 
    //    {
            
    //    }

    //    if (ItemType == JiraItemType.Epic)
    //    {
    //        if(itemType !=  JiraItemType.Epic)
    //            throw new JiraItemDomainException($"Not possible to change the Item type from {JiraItemType.Epic.Name} to anyt other type.");
    //    }
    //    else if (ItemType == JiraItemType.Story)
    //    {
    //        if (itemType != JiraItemType.SubTask)
    //            throw new JiraItemDomainException($"Not possible to change the Item type from {JiraItemType.Epic.Name} to {JiraItemType.SubTask.Name}.");
    //    }
    //    else if (ItemType == JiraItemType.Task)
    //    {

    //    }
    //    else if (ItemType == JiraItemType.SubTask)
    //    {

    //    }
    //    else if (ItemType == JiraItemType.Bug)
    //    {

    //    }
    //}
}

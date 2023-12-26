using JiraSample.Common.Enums;

namespace JiraSample.Common.Events;

public class JiraItemCreatedEvent : BaseEvent
{
    public JiraItemCreatedEvent() 
        : base(nameof(JiraItemCreatedEvent))
    {
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public ItemType ItemType { get; set; }
    public string Author { get; set; }
    public string Asignee { get; set; }
    public Guid? ParentId { get; set; } = null;
    public ItemStatus Status { get; set; }
    public DateTime CreatedDateTime { get; set; }
}

namespace JiraSample.Common.Events;

public class JiraItemParentUpdatedEvent : BaseEvent
{
    public JiraItemParentUpdatedEvent()
        : base(nameof(JiraItemParentUpdatedEvent))
    {
    }

    public Guid ParentId { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}

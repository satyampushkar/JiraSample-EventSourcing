namespace JiraSample.Common.Events;

public class JiraItemChildrenUpdatedEvent : BaseEvent
{
    public JiraItemChildrenUpdatedEvent()
        : base(nameof(JiraItemChildrenUpdatedEvent))
    {
    }

    public List<Guid> Children { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}

namespace JiraSample.Common.Events;

public class JiraItemNameUpdatedEvent : BaseEvent
{
    public JiraItemNameUpdatedEvent() 
        : base(nameof(JiraItemNameUpdatedEvent))
    {
    }

    public string Name { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}

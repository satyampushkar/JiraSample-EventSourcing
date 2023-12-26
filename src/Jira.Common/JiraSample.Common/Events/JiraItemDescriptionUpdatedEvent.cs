namespace JiraSample.Common.Events;

public class JiraItemDescriptionUpdatedEvent : BaseEvent
{
    public JiraItemDescriptionUpdatedEvent()
        : base(nameof(JiraItemDescriptionUpdatedEvent))
    {
    }

    public string Description { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}

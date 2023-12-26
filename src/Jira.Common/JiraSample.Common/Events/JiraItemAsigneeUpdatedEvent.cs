namespace JiraSample.Common.Events;

public class JiraItemAsigneeUpdatedEvent : BaseEvent
{
    public JiraItemAsigneeUpdatedEvent()
        : base(nameof(JiraItemAsigneeUpdatedEvent))
    {
    }

    public string Asignee { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}


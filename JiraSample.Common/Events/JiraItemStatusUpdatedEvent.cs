using JiraSample.Common.Enums;

namespace JiraSample.Common.Events;

public class JiraItemStatusUpdatedEvent : BaseEvent
{
    public JiraItemStatusUpdatedEvent()
        : base(nameof(JiraItemStatusUpdatedEvent))
    {
    }

    public ItemStatus Status { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}


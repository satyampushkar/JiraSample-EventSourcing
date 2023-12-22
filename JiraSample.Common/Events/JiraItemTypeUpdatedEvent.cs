using JiraSample.Common.Enums;

namespace JiraSample.Common.Events;

public class JiraItemTypeUpdatedEvent : BaseEvent
{
    public JiraItemTypeUpdatedEvent()
        : base(nameof(JiraItemTypeUpdatedEvent))
    {
    }

    public ItemType ItemType { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}

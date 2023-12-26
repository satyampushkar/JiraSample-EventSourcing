namespace JiraSample.Common.Enums;

public class ItemStatus : Enumeration
{
    public static ItemStatus ToDo = new(1, nameof(ToDo));
    public static ItemStatus InProgress = new(2, nameof(InProgress));
    public static ItemStatus Done = new(3, nameof(Done));
    public static ItemStatus InTesting = new(4, nameof(InTesting));
    public static ItemStatus InReview = new(5, nameof(InReview));
    public static ItemStatus Blocked = new(6, nameof(Blocked));
    public static ItemStatus Cancelled = new(7, nameof(Cancelled));

    public ItemStatus(int id, string name)
        : base(id, name)
    {
    }
}
namespace JiraSample.Common.Enums;

public class ItemType : Enumeration
{
    public static ItemType Epic = new(1, nameof(Epic));
    public static ItemType Story = new(2, nameof(Story));
    public static ItemType Task = new(3, nameof(Task));
    public static ItemType SubTask = new(4, nameof(SubTask));
    public static ItemType Bug = new(5, nameof(Bug));

    public ItemType(int id, string name) : base(id, name)
    {
    }
}

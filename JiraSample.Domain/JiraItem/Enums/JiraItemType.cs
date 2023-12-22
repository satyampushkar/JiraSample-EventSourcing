using JiraSample.Command.Domain.JiraItem.Exceptions;
using JiraSample.Domain.SeedWork;

namespace JiraSample.Domain.JiraItem.Enums;

public class JiraItemType : Enumeration
{
    public static JiraItemType Epic = new(1, nameof(Epic));
    public static JiraItemType Story = new(2, nameof(Story));
    public static JiraItemType Task = new(3, nameof(Task));
    public static JiraItemType SubTask = new(4, nameof(SubTask));
    public static JiraItemType Bug = new(5, nameof(Bug));

    public JiraItemType(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<JiraItemType> List() =>
        new[] { Epic, Story, Task, SubTask, Bug };

    public static JiraItemType FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new JiraItemDomainException($"Possible values for JiraItemType: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}
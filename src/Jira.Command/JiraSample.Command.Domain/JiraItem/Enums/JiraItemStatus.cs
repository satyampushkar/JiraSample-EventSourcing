using JiraSample.Command.Domain.JiraItem.Exceptions;
using JiraSample.Domain.SeedWork;

namespace JiraSample.Domain.JiraItem.Enums;

public class JiraItemStatus : Enumeration
{
    public static JiraItemStatus ToDo = new(1, nameof(ToDo));
    public static JiraItemStatus InProgress = new(2, nameof(InProgress));
    public static JiraItemStatus Done = new(3, nameof(Done));
    public static JiraItemStatus InTesting = new(4, nameof(InTesting));
    public static JiraItemStatus InReview = new(5, nameof(InReview));
    public static JiraItemStatus Blocked = new(6, nameof(Blocked));
    public static JiraItemStatus Cancelled = new(7, nameof(Cancelled));

    public JiraItemStatus(int id, string name) 
        : base(id, name)
    {
    }

    public static IEnumerable<JiraItemStatus> List() =>
        new[] { ToDo, InProgress, Done, InTesting, InReview, Blocked, Cancelled };

    public static JiraItemStatus FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new JiraItemDomainException($"Possible values for JiraItemStatus: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}
using JiraSample.Query.Domain.Entities.Enums;

namespace JiraSample.Query.Domain.Entities;

public class JiraItemEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public JiraItemType ItemType { get;  set; }
    public string Author { get;  set; }
    public string Asignee { get;  set; }
    public Guid? ParentId { get;  set; } = null;
    public JiraItemStatus ItemStatus { get;  set; }
    //public List<Guid> Children { get;  set; } 
    public DateTime CreatedDateTime { get;  set; }
    public DateTime UpdatedDateTime { get;  set; }
}

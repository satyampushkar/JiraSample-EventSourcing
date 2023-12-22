using JiraSample.Query.Domain.Entities.Enums;

namespace JiraSample.Query.Domain.Entities;

public class JiraItemHistoryEntity
{
    public int Id { get; set; }
    public Guid JiraItemId { get; set; }
    //public ActionPerformed ActionPerformed { get; set; }
    public string ActionPerformed { get; set; }
    public string ChangedValue { get; set; }
    public DateTime ActionPerformedAt { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace CmScheme.WorkAllocation.Core.Entities;

public class TaskDependency
{
    [Key]
    public int TaskDependencyId { get; set; }
    public int WorkAllocationId { get; set; }
    public int DependsOnWorkAllocationId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}

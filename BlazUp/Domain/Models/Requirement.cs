using Domain.Abstractions.Workflows;
using Domain.Models.Lookups;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class Requirement : IEntityState, IEntityPriority {
    //-------------------------PROPERTIES-------------------------
    public long ReqId { get; set; }
    public string ReqName { get; set; } = null!;
    public string ReqDescription { get; set; } = null!;
    public DateTime? DeadLine { get; set; }
    public decimal Progress { get; set; }
    public int ProjectId { get; set; }
    public int? ReqTypeId { get; set; }
    public int? PriorityId { get; set; }
    public int? ReqStateId { get; set; }
    public int? CreatedById { get; set; }

    //-------------------------BEHAVIORS-------------------------
    [NotMapped]
    public int? StateId { get => ReqStateId; set => ReqStateId = value; }
    [NotMapped]
    public int? PriorityLevelId { get => PriorityId; set => PriorityId = value; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual UserInfo? CreatedBy { get; set; }
    public virtual LevelPriority? Priority { get; set; }
    public virtual Project Project { get; set; } = null!;
    public virtual StateEntity? ReqState { get; set; }
    public virtual RequirementType? ReqType { get; set; }
    public virtual ICollection<TaskApp> Tasks { get; set; } = new List<TaskApp>();
}

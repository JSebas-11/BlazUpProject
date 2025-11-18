using Domain.Abstractions.Workflows;
using Domain.Models.Lookups;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class TaskApp : IEntityState, IEntityPriority {
    //-------------------------PROPERTIES-------------------------
    public long TaskId { get; set; }
    public string TaskName { get; set; } = null!;
    public DateTime InitialDate { get; set; }
    public DateTime? DeadLine { get; set; }
    public string TaskDescription { get; set; } = null!;
    public decimal Progress { get; set; }
    public long RequirementId { get; set; }
    public int? PriorityId { get; set; }
    public int? TaskStateId { get; set; }
    public int? CreatedById { get; set; }

    //-------------------------BEHAVIORS-------------------------
    [NotMapped]
    public int? StateId { get => TaskStateId; set => TaskStateId = value; }
    [NotMapped]
    public int? PriorityLevelId { get => PriorityId; set => PriorityId = value; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual UserInfo? CreatedBy { get; set; }
    public virtual LevelPriority? Priority { get; set; }
    public virtual Requirement Requirement { get; set; } = null!;
    public virtual StateEntity? TaskState { get; set; }
    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
}

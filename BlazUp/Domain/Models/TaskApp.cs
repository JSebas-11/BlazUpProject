using Domain.Models.Lookups;

namespace Domain.Models;

public partial class TaskApp {
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

    //-------------------------RELATIONSHIPS-------------------------
    public virtual UserInfo? CreatedBy { get; set; }
    public virtual LevelPriority? Priority { get; set; }
    public virtual Requirement Requirement { get; set; } = null!;
    public virtual StateEntity? TaskState { get; set; }
    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
}

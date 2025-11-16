using Domain.Models.Lookups;

namespace Domain.Models;

public partial class Goal {
    //-------------------------PROPERTIES-------------------------
    public long GoalId { get; set; }
    public string GoalName { get; set; } = null!;
    public string GoalDescription { get; set; } = null!;
    public int? GoalStateId { get; set; }
    public int? PriorityId { get; set; }
    public int ProjectId { get; set; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual StateEntity? GoalState { get; set; }
    public virtual LevelPriority? Priority { get; set; }
    public virtual Project Project { get; set; } = null!;
}

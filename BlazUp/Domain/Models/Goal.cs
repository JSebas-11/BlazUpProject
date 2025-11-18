using Domain.Abstractions.Workflows;
using Domain.Models.Lookups;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public partial class Goal : IEntityState, IEntityPriority {
    //-------------------------PROPERTIES-------------------------
    public long GoalId { get; set; }
    public string GoalName { get; set; } = null!;
    public string GoalDescription { get; set; } = null!;
    public int? GoalStateId { get; set; }
    public int? PriorityId { get; set; }
    public int ProjectId { get; set; }

    //-------------------------BEHAVIORS-------------------------
    [NotMapped]
    public int? StateId { get => GoalStateId; set => GoalStateId = value; }
    [NotMapped]
    public int? PriorityLevelId { get => PriorityId; set => PriorityId = value; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual StateEntity? GoalState { get; set; }
    public virtual LevelPriority? Priority { get; set; }
    public virtual Project Project { get; set; } = null!;
}

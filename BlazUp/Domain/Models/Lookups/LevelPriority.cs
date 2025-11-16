namespace Domain.Models.Lookups;

public partial class LevelPriority {
    //-------------------------PROPERTIES-------------------------
    public int PriorityId { get; set; }
    public string PriorityDescription { get; set; } = null!;

    //-------------------------RELATIONSHIPS-------------------------
    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
    public virtual ICollection<TaskApp> Tasks { get; set; } = new List<TaskApp>();
}

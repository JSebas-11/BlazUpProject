namespace Domain.Models.Lookups;

public partial class StateEntity {
    //-------------------------PROPERTIES-------------------------
    public int EntStateId { get; set; }
    public string EntStateDescription { get; set; } = null!;

    //-------------------------RELATIONSHIPS-------------------------
    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
    public virtual ICollection<TaskApp> Tasks { get; set; } = new List<TaskApp>();
}

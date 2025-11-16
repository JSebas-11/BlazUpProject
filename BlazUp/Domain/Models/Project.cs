using Domain.Models.Lookups;

namespace Domain.Models;

public partial class Project {
    //-------------------------PROPERTIES-------------------------
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public string ProjectDescription { get; set; } = null!;
    public DateTime InitialDate { get; set; }
    public DateTime? DeadLine { get; set; }
    public decimal Progress { get; set; }
    public int? ProjectStateId { get; set; }
    public int? CreatedById { get; set; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual UserInfo? CreatedBy { get; set; }
    public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    public virtual StateEntity? ProjectState { get; set; }
    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
}

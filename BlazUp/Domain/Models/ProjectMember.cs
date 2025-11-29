namespace Domain.Models;

public partial class ProjectMember {
    //-------------------------PROPERTIES-------------------------
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public DateTime JoinedOn { get; set; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual Project Project { get; set; } = null!;
    public virtual UserInfo User { get; set; } = null!;

    //-------------------------CTOR-------------------------
    public ProjectMember() { }
    public ProjectMember(int projectId, int userId, DateTime? joinedOn = null) {
        ProjectId = projectId;
        UserId = userId;
        JoinedOn = joinedOn ?? DateTime.Now;
    }
    public ProjectMember(Project project, UserInfo user, DateTime? joinedOn = null) {
        Project = project;
        User = user;
        JoinedOn = joinedOn ?? DateTime.Now;
    }
}

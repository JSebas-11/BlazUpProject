namespace Domain.Models;

public partial class ProjectMember {
    //-------------------------PROPERTIES-------------------------
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public DateTime JoinedOn { get; set; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual Project Project { get; set; } = null!;
    public virtual UserInfo User { get; set; } = null!;
}

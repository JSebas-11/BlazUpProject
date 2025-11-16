using Domain.Models.Lookups;

namespace Domain.Models;

public partial class UserInfo {
    //-------------------------PROPERTIES-------------------------
    public int UserInfoId { get; set; }
    public string Dni { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public int? RoleId { get; set; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
    public virtual UserRole? Role { get; set; }
    public virtual ICollection<TaskApp> Tasks { get; set; } = new List<TaskApp>();
    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
    public virtual ICollection<NotificationApp> Notifications { get; set; } = new List<NotificationApp>();
}

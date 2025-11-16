namespace Domain.Models;

public partial class UserTask {
    //-------------------------PROPERTIES-------------------------
    public long TaskId { get; set; }
    public int UserId { get; set; }
    public DateTime AssignedOn { get; set; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual TaskApp Task { get; set; } = null!;
    public virtual UserInfo User { get; set; } = null!;
}

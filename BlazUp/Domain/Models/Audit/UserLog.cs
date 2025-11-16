namespace Domain.Models.Audit;

public partial class UserLog {
    //-------------------------PROPERTIES-------------------------
    public int UserLogId { get; set; }
    public int UserId { get; set; }
    public DateTime? RegisteredAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

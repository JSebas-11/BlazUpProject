using Domain.Models.Lookups;

namespace Domain.Models;

public partial class NotificationApp {
    //-------------------------PROPERTIES-------------------------
    public long NotificationId { get; set; }
    public string NotificationTitle { get; set; } = null!;
    public string NotificationMessage { get; set; } = null!;
    public int? NotStateId { get; set; }
    public DateTime CreatedAt { get; set; }

    //-------------------------RELATIONSHIPS-------------------------
    public virtual StateNotification? NotState { get; set; }
    public virtual ICollection<UserInfo> Users { get; set; } = new List<UserInfo>();
}

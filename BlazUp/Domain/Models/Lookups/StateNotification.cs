namespace Domain.Models.Lookups;

public partial class StateNotification {
    //-------------------------PROPERTIES-------------------------
    public int NotStateId { get; set; }
    public string NotStateDescription { get; set; } = null!;

    //-------------------------RELATIONSHIPS-------------------------
    public virtual ICollection<NotificationApp> NotificationApps { get; set; } = new List<NotificationApp>();
}

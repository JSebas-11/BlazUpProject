namespace Domain.Models.Lookups;

public partial class UserRole {
    //-------------------------PROPERTIES-------------------------
    public int UserRoleId { get; set; }
    public string UserRoleDescription { get; set; } = null!;

    //-------------------------RELATIONSHIPS-------------------------
    public virtual ICollection<UserInfo> UserInfos { get; set; } = new List<UserInfo>();
}

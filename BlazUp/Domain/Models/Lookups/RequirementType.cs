namespace Domain.Models.Lookups;

public partial class RequirementType {
    //-------------------------PROPERTIES-------------------------
    public int ReqTypeId { get; set; }
    public string ReqTypeDescription { get; set; } = null!;

    //-------------------------RELATIONSHIPS-------------------------
    public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
}

using Domain.Common.Enums;

namespace Presentation.Models.Dtos;

public class ProjectDto {
    //-------------------------PROPERTIES-------------------------
    public int ProjectId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime InitialDate { get; set; }
    public DateTime? DeadLine { get; set; }
    public double Progress { get; set; }
    public StateEntity State { get; set; }
    public int? CreatedById { get; set; }
    public string CreatorName { get; set; } = null!;
    public int MembersCount { get; set; }
}

using Domain.Common.Enums;
using Domain.Models;
using Presentation.Models.Dtos;

namespace Presentation.Mappers;

internal static class ProjectMapper {
    public static ProjectDto ToDto(Project project)
        => new ProjectDto() {
            ProjectId = project.ProjectId,
            Name = project.ProjectName,
            Description = project.ProjectDescription,
            InitialDate = project.InitialDate,
            DeadLine = project.DeadLine,
            Progress = (double)project.Progress, //double ya que es el requerido por MudProgressBar
            State = project.StateId is null ? StateEntity.Pending : (StateEntity)project.StateId,
            CreatedById = project.CreatedById,
            CreatorName = project.CreatorName ?? "Undefined",
            MembersCount = project.MembersCount
        };
}

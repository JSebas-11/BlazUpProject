using Domain.Common;
using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IProjectRepository {
    Task<IReadOnlyList<Project>> GetByIdMultipleAsync(List<int> projectsId);
    Task<Project?> GetByIdAsync(int projectId);
    Task<Result> InsertAsync(Project project);
    Task<Result> UpdateAsync(Project project);
    Task<Result> DeleteAsync(int projectId);
}

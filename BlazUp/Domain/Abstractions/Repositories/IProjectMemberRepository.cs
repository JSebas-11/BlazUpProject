using Domain.Common;
using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IProjectMemberRepository {
    Task<ProjectMember?> GetByIdsAsync(int projectId, int userId);
    Task<bool> IsMemberAsync(int projectId, int userId);
    Task<IReadOnlyList<int>> GetMembersByProjectAsync(int projectId);
    Task<IReadOnlyList<int>> GetProjectsByMemberAsync(int memberId);
    Task<Result> InsertAsync(ProjectMember pMember);
    Task<Result> InsertRangeAsync(IEnumerable<ProjectMember> pMembers);
    Task<Result> DeleteAsync(int projectId, int userId);
}

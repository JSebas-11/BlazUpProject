using Domain.Common;
using Domain.Common.Enums;
using Domain.Models;

namespace Domain.Abstractions.Services.Entities;

public interface IProjectService {
    // PROJECT
    Task<Project> GetByIdAsync(int projectId);
    Task<IReadOnlyList<Project>> GetByUserAsync(int userId);
    // Retornan projectId
    Task<GenericResult<int>> CreateAsync(string name, string description, DateTime? initialDate, StateEntity state, int creatorId, DateTime? deadline);
    Task<GenericResult<int>> CreateWithMembersAsync(string name, string description, DateTime? initialDate, StateEntity state, int creatorId, DateTime? deadline, IEnumerable<int> membersId);
    Task<Result> UpdateAsync(Project project);

    // MEMBERS
    Task<Result> GetMembersByProjectAsync(int projectId);
    Task<Result> AddMemberAsync(int projectId, int userId);
    Task<Result> AddMembersAsync(int projectId, IEnumerable<int> userIds, bool commit = false);
    Task<Result> RemoveMemberAsync(int projectId, int userId);
}
using Domain.Abstractions.Repositories;
using Domain.Common;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class ProjectMemberRepository : IProjectMemberRepository {
    //------------------------INITIALIZATION------------------------
    private readonly BlazUpProjectContext _context;
    private readonly DbSet<ProjectMember> _table;
    public ProjectMemberRepository(BlazUpProjectContext context) {
        _context = context;
        _table = _context.ProjectMembers;
    }

    //------------------------METHODS------------------------
    #region ReadMethods
    public Task<ProjectMember?> GetByIdsAsync(int projectId, int userId)
        => _table.AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);

    public Task<bool> IsMemberAsync(int projectId, int userId)
        => _table.AsNoTracking()
                .AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);

    public async Task<IReadOnlyList<int>> GetMembersByProjectAsync(int projectId)
        => await _table.AsNoTracking()
                .Where(pm => pm.ProjectId == projectId)
                .Select(pm => pm.UserId).ToListAsync();

    public async Task<IReadOnlyList<int>> GetProjectsByMemberAsync(int memberId)
        => await _table.AsNoTracking()
                .Where(pm => pm.UserId == memberId)
                .Select(pm => pm.ProjectId).ToListAsync();
    #endregion

    #region CrudMethods
    public async Task<Result> InsertAsync(ProjectMember pMember) {
        try {
            await _table.AddAsync(pMember);
            return Result.Ok("Member added succesfully (Waiting for Commit)");
        }
        catch (Exception ex) { return Result.Fail("There has been an error adding member", ex.Message); }
    }

    public async Task<Result> InsertRangeAsync(IEnumerable<ProjectMember> pMembers) {
        try {
            await _table.AddRangeAsync(pMembers);
            return Result.Ok("Members added succesfully (Waiting for Commit)");
        }
        catch (Exception ex) { return Result.Fail("There has been an error adding members", ex.Message); }
    }
    public async Task<Result> DeleteAsync(int projectId, int userId) {
        try {
            if (! await _table.AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId)) 
                return Result.Fail("Project Member does not exits");

            _table.Remove(new ProjectMember() { ProjectId = projectId, UserId = userId }); 
            return Result.Ok("ProjectMember deleted sucessfully (Waiting for Commit)");
        }
        catch (Exception ex) { return Result.Fail("There has been an error deleting projectMember", ex.Message); }
    }
    #endregion
}
using Domain.Abstractions.Repositories;
using Domain.Common;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class ProjectRepository : IProjectRepository {
    //------------------------INITIALIZATION------------------------
    private readonly BlazUpProjectContext _context;
    private readonly DbSet<Project> _table;
    public ProjectRepository(BlazUpProjectContext context) {
        _context = context;
        _table = _context.Projects;
    }

    //------------------------METHODS------------------------
    #region ReadMethods
    public Task<Project?> GetByIdAsync(int projectId)
        => _table.FirstOrDefaultAsync(p => p.ProjectId == projectId);

    public async Task<IReadOnlyList<Project>> GetByIdMultipleAsync(List<int> projectsId)
        => await _table.AsNoTracking()
                .Where(p => projectsId.Contains(p.ProjectId)).ToListAsync();
    #endregion
    
    #region CrudMethods
    public async Task<Result> InsertAsync(Project project) {
        try {
            await _table.AddAsync(project);
            return Result.Ok("Project added succesfully (Waiting for Commit)");
        }
        catch (Exception ex) { return Result.Fail("There has been an error adding project", ex.Message); }
    }

    public async Task<Result> UpdateAsync(Project project) {
        try {
            _table.Attach(project);
            _table.Entry(project).State = EntityState.Modified;

            return Result.Ok($"{project.ProjectName} updated succesfully (Waiting for Commit)");
        } catch (Exception ex) {
            return Result.Fail("There has been an error updating project", ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int projectId) {
        try {
            if (!await _table.AnyAsync(p => p.ProjectId == projectId)) 
                return Result.Fail($"Project with id ({projectId}) does not exist");

            _table.Remove(new Project() { ProjectId = projectId });
            return Result.Ok("Project deleted sucessfully (Waiting for Commit)");
        }
        catch (Exception ex) { return Result.Fail("There has been an error deleting project", ex.Message); }
    }
    #endregion
}

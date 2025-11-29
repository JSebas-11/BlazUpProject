using Domain.Abstractions.Services.Entities;
using Domain.Abstractions.UnitOfWork;
using Domain.Builders;
using Domain.Common;
using Domain.Common.Enums;
using Domain.Models;
using System.Data;

namespace Application.Services;

internal class ProjectService : IProjectService {
    //------------------------INITIALIZATION------------------------
    private readonly IUnitOfWork _unitOfWork;
    public ProjectService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    //------------------------METHODS------------------------
    // PROJECTS
    #region ReadMethods
    public Task<Project> GetByIdAsync(int projectId)
    {
        throw new NotImplementedException();
    }
    public Task<IReadOnlyList<Project>> GetByUserAsync(int userId)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region CrudMethods
    public async Task<GenericResult<int>> CreateAsync(string name, string description, DateTime? initialDate, StateEntity state, int creatorId, DateTime? deadline) {
        //Creacion del proyecto en metodo auxiliar
        GenericResult<Project> projectResult = BuildProject(name, description, initialDate, state, creatorId, deadline);
        if (!projectResult.Success) return GenericResult<int>.Fail(projectResult.Description);

        Project? newProject = projectResult.Value;
        if (newProject is null) return GenericResult<int>.Fail("Error, project null");

        Result operations = await _unitOfWork.Projects.InsertAsync(newProject);
        if (!operations.Success) return GenericResult<int>.Fail($"Error creating Project ({newProject.ProjectName})");
        //Insertar en tabla y concretar el commit
        operations = await _unitOfWork.CommitAsync();
        if (!operations.Success) return GenericResult<int>.Fail($"Error creating Project ({newProject.ProjectName})");

        return GenericResult<int>.Ok($"{newProject.ProjectName} has been created successfully", newProject.ProjectId);
    }

    public async Task<GenericResult<int>> CreateWithMembersAsync(string name, string description, DateTime? initialDate, StateEntity state, int creatorId, DateTime? deadline, IEnumerable<int> membersId) {
        //Creacion del proyecto en metodo auxiliar
        GenericResult<Project> projectResult = BuildProject(name, description, initialDate, state, creatorId, deadline);
        if (!projectResult.Success) return GenericResult<int>.Fail(projectResult.Description);

        Project? newProject = projectResult.Value;
        if (newProject is null) return GenericResult<int>.Fail("Error, project null");

        //Inserccion de proyecto
        Result operations = await _unitOfWork.Projects.InsertAsync(newProject);
        if (!operations.Success) return GenericResult<int>.Fail($"Error creating Project ({newProject.ProjectName})");

        //Commit del proyecto (unitOfWork roto temporalmente)
        operations = await _unitOfWork.CommitAsync();
        if (!operations.Success) return GenericResult<int>.Fail("There has been an error doing the commit");
        
        //Inserccion de miembros (con commit)
        operations = await AddMembersAsync(newProject.ProjectId, membersId, true);
        if (!operations.Success) return GenericResult<int>.Fail(operations.Description);


        return GenericResult<int>.Ok($"{newProject.ProjectName} has been created with its members successfully", newProject.ProjectId);
    }

    public Task<Result> UpdateAsync(Project project)
    {
        throw new NotImplementedException();
    }
    #endregion

    // MEMBERS
    #region ReadMethods
    public Task<Result> GetMembersByProjectAsync(int projectId)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region CrudMethods
    public Task<Result> AddMemberAsync(int projectId, int userId)
    {
        throw new NotImplementedException();
    }
    public async Task<Result> AddMembersAsync(int projectId, IEnumerable<int> userIds, bool commit = false) {
        Result operations;
        //Validaciones
        if (userIds is null || userIds.Count() == 0) return Result.Fail("No members were provided");

        //Obtener miembros actuales del proyecto
        var existingMembers = await _unitOfWork.ProjectMembers.GetMembersByProjectAsync(projectId);

        //Filtrar usuarios repetidos
        var newUserIds = userIds.Except(existingMembers).ToList();
        if (newUserIds.Count == 0)
            return Result.Fail("All provided users are already part of the project");

        //Creacion de los ProjectMembers
        IEnumerable<ProjectMember> members = newUserIds.Select(userId => new ProjectMember(projectId, userId));

        operations = await _unitOfWork.ProjectMembers.InsertRangeAsync(members);
        if (!operations.Success)
            return Result.Fail(operations.Description);

        if (commit) {
            operations = await _unitOfWork.CommitAsync();
            if (!operations.Success)
                return Result.Fail(operations.Description);    
        }

        return Result.Ok("Members have been added successfully");
    }
    public Task<Result> RemoveMemberAsync(int projectId, int userId)
    {
        throw new NotImplementedException();
    }
    #endregion

    //------------------------innerMeths------------------------
    private GenericResult<Project> BuildProject(string name, string description, DateTime? initialDate, StateEntity state, int creatorId, DateTime? deadline) {
        try {
            //Creacion de proyecto mediante el builder definido en dominio
            Project newProject = new ProjectBuilder()
                    .WithGeneralInfo(name, description)
                    .WithInitialDate(initialDate)
                    .WithDeadline(deadline)
                    .WithState(state)
                    .WithCreator(creatorId)
                    .Build();

            return GenericResult<Project>.Ok("Project Builded successfully", newProject);
        }
        catch (Exception ex) { return GenericResult<Project>.Fail($"There has been an error creating project:\n{ex.Message}"); }
    }
}

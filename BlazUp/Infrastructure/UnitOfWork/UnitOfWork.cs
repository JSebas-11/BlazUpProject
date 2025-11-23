using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitOfWork;
using Domain.Common;
using Domain.Models;
using Domain.Models.Lookups;
using Infrastructure.Data;

namespace Infrastructure.UnitOfWork;

internal class UnitOfWork : IUnitOfWork {
    //------------------------INITIALIZATION------------------------
    private readonly BlazUpProjectContext _context;
    public UnitOfWork(BlazUpProjectContext context,
        IReadOnlyRepository<LevelPriority> levelPriorityRepository,
        IReadOnlyRepository<RequirementType> requirementTypeRepository,
        IReadOnlyRepository<StateEntity> stateEntityRepository,
        IReadOnlyRepository<UserRole> userRoleRepository,
        IUserRepository userRepository
    ) {
        _context = context;
        LevelPriorities = levelPriorityRepository;
        RequirementTypes = requirementTypeRepository;
        StateEntities = stateEntityRepository;
        UserRoles = userRoleRepository;
        Users = userRepository;
    }

    //------------------------PROPERTIES------------------------
    public IReadOnlyRepository<LevelPriority> LevelPriorities { get; }
    public IReadOnlyRepository<RequirementType> RequirementTypes { get; }
    public IReadOnlyRepository<StateEntity> StateEntities { get; }
    public IReadOnlyRepository<UserRole> UserRoles { get; }

    public IUserRepository Users { get; }

    public IRepository<Goal> Goals => throw new NotImplementedException();
    public IRepository<NotificationApp> Notifications => throw new NotImplementedException();
    public IRepository<Project> Projects => throw new NotImplementedException();
    public IRepository<ProjectMember> ProjectMembers => throw new NotImplementedException();
    public IRepository<Requirement> Requirements => throw new NotImplementedException();
    public IRepository<TaskApp> Tasks => throw new NotImplementedException();
    public IRepository<UserTask> UserTasks => throw new NotImplementedException();

    //------------------------METHODS------------------------
    public async Task<Result> CommitAsync() {
        try {
            await _context.SaveChangesAsync();
            return Result.Ok("Changes has been applied successfully");
        }
        catch (Exception ex) { 
            return Result.Fail("There has been an error applied changes", ex.Message); 
        }
    }

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
}

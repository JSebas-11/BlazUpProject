using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Models.Lookups;

namespace Domain.Abstractions.UnitOfWork;

//Interface que expondra los Repositories a los Services y les permitira ser tratados como unidad
public interface IUnitOfWork {
    //---------------------PROPERTIES---------------------
    //---------------------readOnly---------------------
    public IReadOnlyRepository<LevelPriority> LevelPriorities { get; }
    public IReadOnlyRepository<RequirementType> RequirementTypes { get; }
    public IReadOnlyRepository<StateEntity> StateEntities { get; }
    public IReadOnlyRepository<StateNotification> StateNotifications { get; }
    public IReadOnlyRepository<UserRole> UserRoles { get; }
    
    //---------------------full---------------------
    public IRepository<Goal> Goals { get; }
    public IRepository<NotificationApp> Notifications { get; }
    public IRepository<Project> Projects { get; }
    public IRepository<ProjectMember> ProjectMembers { get; }
    public IRepository<Requirement> Requirements { get; }
    public IRepository<TaskApp> Tasks { get; }
    public IRepository<UserInfo> Users { get; }
    public IRepository<UserTask> UserTasks { get; }

    //---------------------METHODS---------------------
    Task<Common.Result> CommitAsync();
}

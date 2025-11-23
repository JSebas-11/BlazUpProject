using Domain.Abstractions.Services.Entities;
using Domain.Abstractions.Services.Facade;

namespace Application.Facade;

public class DataService : IDataService {
    //------------------------INITIALIZATION------------------------
    public DataService(
        ICatalogService catalogService,
        IUserService userService
    ) {
        Catalogs = catalogService;
        Users = userService;
    }

    //------------------------SERVICES------------------------
    public ICatalogService Catalogs { get; }
    public IGoalService Goals { get; }
    public INotificationService Notifications { get; }
    public IProjectMemberService ProjectMembers { get; }
    public IProjectService Projects { get; }
    public IRequirementService Requirements { get; }
    public ITaskService Tasks { get; }
    public IUserNotificationService UserNotifications { get; }
    public IUserService Users { get; }
    public IUserTaskService UserTasks { get; }
}

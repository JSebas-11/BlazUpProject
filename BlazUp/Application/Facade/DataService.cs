using Domain.Abstractions.Services.Entities;
using Domain.Abstractions.Services.Facade;

namespace Application.Facade;

internal class DataService : IDataService {
    //------------------------INITIALIZATION------------------------
    public DataService(
        ICatalogService catalogService,
        IProjectService projectService,
        IUserService userService
    ) {
        Catalogs = catalogService;
        Projects = projectService;
        Users = userService;
    }

    //------------------------SERVICES------------------------
    public ICatalogService Catalogs { get; }
    public IGoalService Goals { get; }
    public INotificationService Notifications { get; }
    public IProjectService Projects { get; }
    public IRequirementService Requirements { get; }
    public ITaskService Tasks { get; }
    public IUserNotificationService UserNotifications { get; }
    public IUserService Users { get; }
    public IUserTaskService UserTasks { get; }
}

using Domain.Abstractions.Services.Entities;
using Domain.Abstractions.Services.Facade;

namespace Application.Facade;

public class DataService : IDataService {
    public ICatalogService Catalogs => throw new NotImplementedException();

    public IGoalService Goals => throw new NotImplementedException();

    public INotificationService Notifications => throw new NotImplementedException();

    public IProjectMemberService ProjectMembers => throw new NotImplementedException();

    public IProjectService Projects => throw new NotImplementedException();

    public IRequirementService Requirements => throw new NotImplementedException();

    public ITaskService Tasks => throw new NotImplementedException();

    public IUserNotificationService UserNotifications => throw new NotImplementedException();

    public IUserService Users => throw new NotImplementedException();

    public IUserTaskService UserTasks => throw new NotImplementedException();
}

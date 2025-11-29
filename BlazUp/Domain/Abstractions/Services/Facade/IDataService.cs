using Domain.Abstractions.Services.Entities;

namespace Domain.Abstractions.Services.Facade;

//Declaracion de interface IDataService que sera inyectada a traves de las paginas
//  para gestionar los datos y toda la logica de negocio a traves de los servicios
public interface IDataService {
    //---------------------PROPERTIES---------------------
    //---------------------services---------------------
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

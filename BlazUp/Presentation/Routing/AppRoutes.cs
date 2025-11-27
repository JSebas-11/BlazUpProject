namespace Presentation.Routing;

internal class AppRoutes {
    #region Common
    public const string Login = "/";
    public const string Register = "/register";
    public const string Home = "/home";
    public const string Projects = "/projects";
    public const string Configuration = "/configuration";
    public const string Notifications = "/notifications";
    #endregion

    #region AdminPages
    public const string AdminHistory = "admin/history";
    public const string AdminRequests = "admin/request";
    #endregion
    
    #region DeveloperPages
    public const string DeveloperRequirements = "developer/requirements";
    public const string DeveloperTasks = "developer/tasks";
    #endregion
}
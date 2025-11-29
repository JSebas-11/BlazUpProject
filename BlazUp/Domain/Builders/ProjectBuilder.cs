using Domain.Common.Enums;
using Domain.Models;

namespace Domain.Builders;

public class ProjectBuilder {
    //-------------------------INITIALIZATION-------------------------
    private readonly Project _project;
    public ProjectBuilder() => _project = new Project() {
        InitialDate = DateTime.Now, Progress = 0, ProjectStateId = (int)StateEntity.InProgress
    };

    //-------------------------METHODS-------------------------
    //-------------------------METHODS-------------------------
    public Project Build() {
        //Validaciones de propiedades antes de crear
        if (_project.ProjectName is null || _project.ProjectDescription is null)
            throw new ArgumentException("Project does not have all properties, it can not be generated");

        return _project;
    }

    #region CreationalBuilders
    public ProjectBuilder WithGeneralInfo(string name, string description) {
        _project.ProjectName = name.Trim();
        _project.ProjectDescription = description.Trim();

        return this;
    }
    public ProjectBuilder WithInitialDate(DateTime? initialDate) {
        if (_project.DeadLine is not null && initialDate >= _project.DeadLine)
            throw new ArgumentException("InitialDate must be earlier than Deadline");
    
        _project.InitialDate = initialDate ?? DateTime.Now;

        return this;
    }
    public ProjectBuilder WithDeadline(DateTime? deadline) {
        if (deadline is not null && deadline <= _project.InitialDate) 
            throw new ArgumentException("Deadline must be later than InitialDate");

        _project.DeadLine = deadline;

        return this;
    }
    public ProjectBuilder WithState(StateEntity state) {
        _project.ProjectStateId = (int)state;

        return this;
    }
    public ProjectBuilder WithCreator(int createdBy) {
        _project.CreatedById = createdBy;

        return this;
    }
    #endregion
}

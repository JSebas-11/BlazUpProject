using Domain.Common;

namespace Domain.Abstractions.Workflows;

//Interfaz para cambiar la prioridad de: Goal, Requirement y TaskApp
public interface IEntityPriority {
    public int? PriorityLevelId { get; set; }
}

public interface IPriorityManager {
    Result ChangePriority<T>(T entity, LevelPriority newPriority) where T : IEntityPriority;
}
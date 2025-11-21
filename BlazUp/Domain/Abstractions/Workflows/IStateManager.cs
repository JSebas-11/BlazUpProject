using Domain.Common;
using Domain.Common.Enums;

namespace Domain.Abstractions.Workflows;

//Interfaz para cambiar los estados de: Project, Goal, Requirement y TaskApp
public interface IEntityState {
    public int? StateId { get; set; }
}

public interface IStateManager {
    Result ChangeState<T>(T entity, StateEntity newState) where T : IEntityState;
}
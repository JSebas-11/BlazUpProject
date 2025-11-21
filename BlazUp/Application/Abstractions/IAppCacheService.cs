using Domain.Models.Lookups;

namespace Application.Abstractions;

public interface IAppCacheService {
    Task<IReadOnlyList<LevelPriority>> GetPrioritiesAsync();
    Task<IReadOnlyList<RequirementType>> GetReqTypesAsync();
    Task<IReadOnlyList<StateEntity>> GetStatesAsync();
    Task<IReadOnlyList<UserRole>> GetRolesAsync();
}

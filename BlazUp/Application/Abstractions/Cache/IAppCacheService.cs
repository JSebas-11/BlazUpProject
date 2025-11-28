using Domain.Models;
using Domain.Models.Lookups;

namespace Application.Abstractions.Cache;

public interface IAppCacheService {
    //------------------------LOOKUPS------------------------
    Task<IReadOnlyList<LevelPriority>> GetPrioritiesAsync();
    Task<IReadOnlyList<RequirementType>> GetReqTypesAsync();
    Task<IReadOnlyList<StateEntity>> GetStatesAsync();
    Task<IReadOnlyList<UserRole>> GetRolesAsync();

    //------------------------USERS------------------------
    Task<IReadOnlyList<UserInfo>> GetUsersAsync();
    void ClearUsersCache();
}

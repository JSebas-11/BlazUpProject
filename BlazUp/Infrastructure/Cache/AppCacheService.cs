using Application.Abstractions.Cache;
using Domain.Abstractions.UnitOfWork;
using Domain.Models;
using Domain.Models.Lookups;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache;

internal class AppCacheService : IAppCacheService {
    //------------------------INITIALIZATION------------------------
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;
    public AppCacheService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) {
        _unitOfWork = unitOfWork;
        _cache = memoryCache;
    }
    
    //------------------------KEYs------------------------
    private const string PRIORITIES_KEY = "catalog:priorities";
    private const string REQTYPES_KEY = "catalog:reqTypes";
    private const string USERROLES_KEY = "catalog:userRoles";
    private const string STATES_KEY = "catalog:states";
    private const string USERS_KEY = "table:users";

    //------------------------METHODS------------------------
    #region LOOKUPS
    public async Task<IReadOnlyList<LevelPriority>> GetPrioritiesAsync() {
        IReadOnlyList<LevelPriority> priorities = [];

        if (!_cache.TryGetValue(PRIORITIES_KEY, out priorities)) {
            priorities = await _unitOfWork.LevelPriorities.GetValuesAsync();
            _cache.Set(PRIORITIES_KEY, priorities);
        }

        return priorities;
    }

    public async Task<IReadOnlyList<RequirementType>> GetReqTypesAsync() {
        IReadOnlyList<RequirementType> types = [];

        if (!_cache.TryGetValue(REQTYPES_KEY, out types)) {
            types = await _unitOfWork.RequirementTypes.GetValuesAsync();
            _cache.Set(REQTYPES_KEY, types);
        }

        return types;
    }

    public async Task<IReadOnlyList<UserRole>> GetRolesAsync() {
        IReadOnlyList<UserRole> roles = [];

        if (!_cache.TryGetValue(USERROLES_KEY, out roles)) {
            roles = await _unitOfWork.UserRoles.GetValuesAsync();
            _cache.Set(USERROLES_KEY, roles);
        }

        return roles;
    }

    public async Task<IReadOnlyList<StateEntity>> GetStatesAsync() {
        IReadOnlyList<StateEntity> states = [];

        if (!_cache.TryGetValue(STATES_KEY, out states)) {
            states = await _unitOfWork.StateEntities.GetValuesAsync();
            _cache.Set(STATES_KEY, states);
        }

        return states;
    }
    #endregion

    #region USERS
    public async Task<IReadOnlyList<UserInfo>> GetUsersAsync() {
        IReadOnlyList<UserInfo> users = [];

        if (!_cache.TryGetValue(USERS_KEY, out users)) {
            users = await _unitOfWork.Users.GetAllAsync();
            _cache.Set(USERS_KEY, users);
        }

        return users;
    }

    public void ClearUsersCache() => _cache.Remove(USERS_KEY);
    #endregion
}

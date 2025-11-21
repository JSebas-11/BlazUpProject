using Application.Abstractions;
using Domain.Abstractions.Services.Entities;
using Domain.Models.Lookups;

namespace Application.Services;

public class CatalogService : ICatalogService {
    //------------------------INITIALIZATION------------------------
    private readonly IAppCacheService _cache;
    public CatalogService(IAppCacheService cacheService)
        => _cache = cacheService;

    //------------------------METHODS------------------------
    public Task<IReadOnlyList<LevelPriority>> GetPrioritiesAsync()
        => _cache.GetPrioritiesAsync();

    public Task<IReadOnlyList<RequirementType>> GetReqTypesAsync()
        => _cache.GetReqTypesAsync();

    public Task<IReadOnlyList<UserRole>> GetRolesAsync()
        => _cache.GetRolesAsync();

    public Task<IReadOnlyList<StateEntity>> GetStatesAsync()
        => _cache.GetStatesAsync();
}
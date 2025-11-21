using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitOfWork;
using Domain.Models.Lookups;
using Infrastructure.Data;
using Infrastructure.Repositories.Lookups;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionStr) {
        // Injecciones de DbContext - Cache - Repositories - UnitOfWork
        // DbContext
        services.AddDbContext<BlazUpProjectContext>(op => op.UseSqlServer(connectionStr), ServiceLifetime.Scoped);

        // Cache
        services.AddSingleton<IMemoryCache, MemoryCache>();

        // Repositories
        services.AddScoped<IReadOnlyRepository<LevelPriority>, LevelPriorityRepository>();
        services.AddScoped<IReadOnlyRepository<RequirementType>, RequirementTypeRepository>();
        services.AddScoped<IReadOnlyRepository<StateEntity>, StateEntityRepository>();
        services.AddScoped<IReadOnlyRepository<UserRole>, UserRoleRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }
}

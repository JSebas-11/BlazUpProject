using Application.Abstractions.Cache;
using Application.Abstractions.Utilities;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.UnitOfWork;
using Domain.Models.Lookups;
using Infrastructure.Cache;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Lookups;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionStr) {
        // Injecciones de DbContext - Cache - Repositories - UnitOfWork
        // DbContext
        services.AddDbContext<BlazUpProjectContext>(op => op.UseSqlServer(connectionStr), ServiceLifetime.Scoped);

        // Cache
        services.AddMemoryCache();
        services.AddScoped<IAppCacheService, AppCacheService>();
        
        // Utilities
        services.AddSingleton<IHasher, Hasher>();

        // Repositories
        services.AddScoped<IReadOnlyRepository<LevelPriority>, LevelPriorityRepository>();
        services.AddScoped<IReadOnlyRepository<RequirementType>, RequirementTypeRepository>();
        services.AddScoped<IReadOnlyRepository<StateEntity>, StateEntityRepository>();
        services.AddScoped<IReadOnlyRepository<UserRole>, UserRoleRepository>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();

        // UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }
}

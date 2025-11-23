using Application.Facade;
using Application.Services;
using Domain.Abstractions.Services.Entities;
using Domain.Abstractions.Services.Facade;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        // Injecciones de Services
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IDataService, DataService>();

        return services;
    }
}
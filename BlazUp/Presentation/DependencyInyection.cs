using Presentation.Services;

namespace Presentation;

public static class DependencyInyection {
    public static IServiceCollection AddPresentation(this IServiceCollection services) {
        // Injecciones de Services y demas utilidades de la capa Presentation
        services.AddSingleton<ThemeService>();


        return services;
    }
}

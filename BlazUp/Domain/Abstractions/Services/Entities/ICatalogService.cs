using Domain.Models.Lookups;

namespace Domain.Abstractions.Services.Entities;

//Clase para agrupar algunos Lookups de uso general en varias entidades
public interface ICatalogService {
    Task<List<LevelPriority>> GetPrioritiesAsync();
    Task<List<StateEntity>> GetStatesAsync();
}

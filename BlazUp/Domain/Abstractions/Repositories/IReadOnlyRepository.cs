namespace Domain.Abstractions.Repositories;

//Interface para entidades que no tendran mas instancias solo se usaran
//las predefinidas en el diseño de la DB
public interface IReadOnlyRepository<T> {
    Task<IReadOnlyList<T>> GetValuesAsync();
    Task<T?> GetValueByIdAsync(int modelId);
}

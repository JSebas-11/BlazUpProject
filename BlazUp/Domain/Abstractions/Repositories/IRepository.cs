using Domain.Common;

namespace Domain.Abstractions.Repositories;

//Interface para centralizar la persistencia y el acceso a datos de los modelos
public interface IRepository<T> {
    Task<IReadOnlyList<T>> GetAllAsync(bool tracking = false);
    Task<T?> GetByIdAsync(int modelId);
    Task<Result> InsertAsync(T model);
    Task<Result> UpdateAsync(T model);
    Task<Result> DeleteAsync(int modelId);
}
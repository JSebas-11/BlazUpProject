using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IUserRepository : IRepository<UserInfo> {
    public Task<bool> ExistsUserAsync(string dni);
    public Task<UserInfo?> GetUserAsync(string dni);
}
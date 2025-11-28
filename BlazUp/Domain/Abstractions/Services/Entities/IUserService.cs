using Domain.Common;
using Domain.Common.Enums;
using Domain.Models;

namespace Domain.Abstractions.Services.Entities;

public interface IUserService {
    public Task<bool> ExistsUserAsync(string dni);
    public Task<UserInfo?> GetUserAsync(string dni, string password);
    public Task<IReadOnlyList<UserInfo>> GetUsersAsync();
    public Task<Result> CreateUserAsync(string dni, string password, string userName, Role role);
    public Task<Result> UpdateUserAsync(UserInfo user);
}

using Domain.Common;
using Domain.Models;

namespace Domain.Abstractions.Services.Entities;

public interface IUserService {
    public Task<bool> ExistsUserAsync(string dni);
    public Task<UserInfo?> GetUserAsync(string dni, string password);
    public Task<Result> CreateUserAsync();
    public Task<Result> UpdateUserAsync(UserInfo user);
    
}

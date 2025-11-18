using Domain.Models.Lookups;

namespace Domain.Abstractions.Services.Entities;

public interface IUserService {
    //-------------------------LOOKUPS-------------------------
    Task<List<UserRole>> GetRolesAsync();
}

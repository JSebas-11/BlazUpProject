using Domain.Models.Lookups;

namespace Domain.Abstractions.Services.Entities;

public interface IRequirementService {
    //-------------------------LOOKUPS-------------------------
    Task<List<RequirementType>> GetTypesAsync();
}

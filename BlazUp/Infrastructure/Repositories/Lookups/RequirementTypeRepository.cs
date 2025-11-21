using Domain.Abstractions.Repositories;
using Domain.Models.Lookups;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Lookups;

public class RequirementTypeRepository : IReadOnlyRepository<RequirementType> {
    //------------------------INITIALIZATION------------------------
    private readonly BlazUpProjectContext _context;
    private readonly DbSet<RequirementType> _table;
    public RequirementTypeRepository(BlazUpProjectContext context) {
        _context = context;
        _table = _context.RequirementTypes;
    }

    //------------------------METHODS------------------------
    public async Task<RequirementType?> GetValueByIdAsync(int reqTypeId)
        => await _table.AsNoTracking().FirstOrDefaultAsync(r => r.ReqTypeId == reqTypeId);

    public async Task<IReadOnlyList<RequirementType>> GetValuesAsync()
        => await _table.AsNoTracking().ToListAsync();
}
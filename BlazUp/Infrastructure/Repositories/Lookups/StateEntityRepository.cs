using Domain.Abstractions.Repositories;
using Domain.Models.Lookups;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Lookups;

internal class StateEntityRepository : IReadOnlyRepository<StateEntity> {
    //------------------------INITIALIZATION------------------------
    private readonly BlazUpProjectContext _context;
    private readonly DbSet<StateEntity> _table;
    public StateEntityRepository(BlazUpProjectContext context) {
        _context = context;
        _table = _context.EntityStates;
    }

    //------------------------METHODS------------------------
    public async Task<StateEntity?> GetValueByIdAsync(int stateId)
        => await _table.AsNoTracking().FirstOrDefaultAsync(s => s.EntStateId == stateId);

    public async Task<IReadOnlyList<StateEntity>> GetValuesAsync()
        => await _table.AsNoTracking().ToListAsync();
}
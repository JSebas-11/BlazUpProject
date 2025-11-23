using Domain.Abstractions.Repositories;
using Domain.Models.Lookups;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Lookups;

internal class LevelPriorityRepository : IReadOnlyRepository<LevelPriority> {
    //------------------------INITIALIZATION------------------------
    private readonly BlazUpProjectContext _context;
    private readonly DbSet<LevelPriority> _table;
    public LevelPriorityRepository(BlazUpProjectContext context) {
        _context = context;
        _table = _context.LevelPriorities;
    }

    //------------------------METHODS------------------------
    public async Task<LevelPriority?> GetValueByIdAsync(int priorityId)
        => await _table.AsNoTracking().FirstOrDefaultAsync(p => p.PriorityId == priorityId);

    public async Task<IReadOnlyList<LevelPriority>> GetValuesAsync()
        => await _table.AsNoTracking().ToListAsync();
}
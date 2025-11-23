using Domain.Abstractions.Repositories;
using Domain.Models.Lookups;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Lookups;

internal class UserRoleRepository : IReadOnlyRepository<UserRole> {
    //------------------------INITIALIZATION------------------------
    private readonly BlazUpProjectContext _context;
    private readonly DbSet<UserRole> _table;
    public UserRoleRepository(BlazUpProjectContext context) {
        _context = context;
        _table = _context.UserRoles;
    }

    //------------------------METHODS------------------------
    public async Task<UserRole?> GetValueByIdAsync(int roleId)
        => await _table.AsNoTracking().FirstOrDefaultAsync(r => r.UserRoleId == roleId);

    public async Task<IReadOnlyList<UserRole>> GetValuesAsync()
        => await _table.AsNoTracking().ToListAsync();
}
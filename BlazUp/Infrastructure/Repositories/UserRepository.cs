using Domain.Abstractions.Repositories;
using Domain.Common;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository {
    //------------------------INITIALIZATION------------------------
    private readonly BlazUpProjectContext _context;
    private readonly DbSet<UserInfo> _table;

    public UserRepository(BlazUpProjectContext context) { 
        _context = context;
        _table = _context.UserInfos;
    }

    //------------------------METHODS------------------------
    #region ReadMethods
    public async Task<IReadOnlyList<UserInfo>> GetAllAsync(bool tracking = false)
        => tracking ? await _table.ToListAsync() : await _table.AsNoTracking().ToListAsync();
    public async Task<UserInfo?> GetByIdAsync(int modelId) => await _table.FindAsync(modelId);
    public Task<bool> ExistsUserAsync(string dni) => _table.AnyAsync(u => u.Dni == dni);
    public Task<UserInfo?> GetUserAsync(string dni) => _table.FirstOrDefaultAsync(u => u.Dni == dni);
    #endregion

    #region CrudMethods
    public async Task<Result> InsertAsync(UserInfo model) {
        try {
            await _table.AddAsync(model);
            return Result.Ok($"{model.UserName} inserted succesfully (Waiting for Commit)");
        }
        catch (Exception ex) { return Result.Fail("There has been an error adding user", ex.Message); }
    }

    public async Task<Result> UpdateAsync(UserInfo model) {
        try {
            UserInfo? userTracked = await GetByIdAsync(model.UserInfoId);
            if (userTracked is null) return Result.Fail($"{model.UserName} was not found in DB");

            //Copiar valores de objeto nuevo (el del parametro) al que esta trackeado
            _table.Entry(userTracked).CurrentValues.SetValues(model);

            if (_table.Entry(userTracked).State == EntityState.Modified)
                return Result.Ok($"{userTracked.UserName} updated succesfully (Waiting for Commit)");

            return Result.Fail($"{userTracked.UserName} was not modified");
        }
        catch (Exception ex) { return Result.Fail("There has been an error updating user", ex.Message); }
    }

    public async Task<Result> DeleteAsync(int modelId) {
        try {
            UserInfo? user = await GetByIdAsync(modelId);
            if (user == null)
                return Result.Fail($"User with id ({modelId}) does not exist");

            _table.Remove(user);
            return Result.Ok($"User with id {modelId} deleted sucessfully (Waiting for Commit)");
        }
        catch (Exception ex) { return Result.Fail("There has been an error deleting user", ex.Message); }
    }
    #endregion
}
using Application.Abstractions.Utilities;
using Domain.Abstractions.Services.Entities;
using Domain.Abstractions.UnitOfWork;
using Domain.Builders;
using Domain.Common;
using Domain.Common.Enums;
using Domain.Models;

namespace Application.Services;

public class UserService : IUserService {
    //------------------------INITIALIZATION------------------------
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHasher _hasher;
    public UserService(IUnitOfWork unitOfWork, IHasher hasher) {
        _unitOfWork = unitOfWork;
        _hasher = hasher;
    }

    //------------------------METHODS------------------------
    #region ReadMethods
    public Task<bool> ExistsUserAsync(string dni) => _unitOfWork.Users.ExistsUserAsync(dni);
    
    public async Task<UserInfo?> GetUserAsync(string dni, string password) {
        UserInfo? user = await _unitOfWork.Users.GetUserAsync(dni);
        if (user is null) return null;

        //Validar si contraseña es correcta, y devolver el usuario en caso de serlo
        return _hasher.VerifyPassword(password, user.PasswordHash) ? user : null;
    }
    #endregion

    #region CrudMethods
    public async Task<Result> CreateUserAsync(string dni, string password, string userName, Role role) {
        Result operations;
        //Creacion de usuario mediante el builder definido en dominio
        UserInfo newUser = new UserBuilder()
                .WithGeneralInfo(dni, userName)
                .WithPassword(_hasher.HashPassword(password))
                .WithRole(role)
                .Build();

        //Insertar en tabla y concretar el commit
        operations = await _unitOfWork.Users.InsertAsync(newUser);
        if (!operations.Success) return Result.Fail($"Error creating User ({userName})");
        operations = await _unitOfWork.CommitAsync();
        if (!operations.Success) return Result.Fail($"Error creating User ({userName})");

        return Result.Ok($"User ({userName}) has been registered successfully");
    }
    public async Task<Result> UpdateUserAsync(UserInfo user) {
        Result operation = await _unitOfWork.Users.UpdateAsync(user);
        if (!operation.Success) return operation;

        return await _unitOfWork.CommitAsync();
    }
    #endregion
}

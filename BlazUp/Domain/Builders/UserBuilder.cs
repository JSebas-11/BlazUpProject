using Domain.Common.Enums;
using Domain.Models;

namespace Domain.Builders;

public class UserBuilder {
    //-------------------------INITIALIZATION-------------------------
    private readonly UserInfo _user;
    public UserBuilder() => _user = new UserInfo();

    //-------------------------METHODS-------------------------
    public UserInfo Build() {
        //Validaciones de propiedades antes de crear
        if (_user.Dni is null || _user.PasswordHash is null || _user.UserName is null)
            throw new ArgumentException("User does not have all properties, it can not be generated");

        if (_user.RoleId is null)
            throw new ArgumentException("User does not have a role defined");

        return _user;
    }

    #region CreationalBuilders
    public UserBuilder WithGeneralInfo(string dni, string userName) {
        _user.Dni = dni.Trim();
        _user.UserName = userName.Trim();

        return this;
    }

    public UserBuilder WithPassword(string hashPassword) {
        _user.PasswordHash = hashPassword;

        return this;
    }

    public UserBuilder WithRole(Role role) {
        _user.RoleId = (int)role;

        return this;
    }
    #endregion
}
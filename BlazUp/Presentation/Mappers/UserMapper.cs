using Domain.Common.Enums;
using Domain.Models;
using Presentation.Models.Dtos;

namespace Presentation.Mappers;

internal static class UserMapper {
    public static UserDto ToDto(UserInfo user)
        => new UserDto() { 
            UserId = user.UserInfoId, 
            UserDni = user.Dni,
            UserName = user.UserName,
            Role = user.RoleId is null ? 0 : (Role)user.RoleId
        };
}

using Domain.Common.Enums;
using Domain.Models;
using Presentation.Mappers;
using Presentation.Models.Dtos;

namespace Presentation.Services;

public class UserContext {
    //-------------------------PROPERTIES-------------------------
    public UserDto? UserDto { get; private set; }

    public bool IsAuthenticated => UserDto is not null;
    public string? UserName => UserDto?.UserName;
    public Role? Role => UserDto?.Role;

    //-------------------------METHODS-------------------------
    public void SetUser(UserInfo user) => UserDto = UserMapper.ToDto(user);
    public void Clear() => UserDto = null;
}
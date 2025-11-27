using Domain.Common.Enums;

namespace Presentation.Models.Dtos;

public class UserDto {
    //-------------------------PROPERTIES-------------------------
    public int UserId { get; set; }
    public string UserDni { get; set; } = null!;
    public string UserName { get; set; }= null!;
    public Role Role { get; set; }
}
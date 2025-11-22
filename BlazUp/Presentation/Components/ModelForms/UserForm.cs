using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Components.ModelForms;

//Comprobacion de la informacion de usuarios recibos en la UI mediante DataAnnotations
public class UserForm {
    //-------------------DNI-------------------
    [Required(ErrorMessage = "DNI is required")]
    [RegularExpression(@"^\d+$", ErrorMessage = "DNI must be a number")]
    [StringLength(ValidationConsts.MaxDniLength, MinimumLength = ValidationConsts.MinDniLength, 
        ErrorMessage = "DNI must have between {2} and {1} digits")]
    public string Dni { get; set; } = string.Empty!.Trim();

    //-------------------USERNAME-------------------
    [Required(ErrorMessage = "UserName is required")]
    [StringLength(ValidationConsts.MaxUserNameLength, MinimumLength = ValidationConsts.MinUserNameLength,
        ErrorMessage = "UserName must have between {2} and {1} digits")]
    public string UserName { get; set; } = string.Empty!.Trim();
    
    //-------------------PASSWORD-------------------
    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(ValidationConsts.PasswordPattern, ErrorMessage = "Incorrect password pattern")]
    public string Password { get; set; } = string.Empty!.Trim();

    //-------------------ROLE-------------------
    [Required(ErrorMessage = "UserRole is required")]
    [Range(1, 3, ErrorMessage = "UserRole must be selected")]
    public int RoleId { get; set; } = 0;
}

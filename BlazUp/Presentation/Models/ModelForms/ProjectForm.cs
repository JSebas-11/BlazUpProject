using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models.ModelForms;

public class ProjectForm {
    //-------------------NAME-------------------
    [Required(ErrorMessage = "Name is required")]
    [StringLength(ValidationConsts.MaxEntityNameLength, MinimumLength = ValidationConsts.MinEntityNameLength,
        ErrorMessage = "ProjectName must have between {2} and {1} digits")]
    public string ProjectName { get; set; } = string.Empty!.Trim();

    //-------------------DESCRIPTION-------------------
    [Required(ErrorMessage = "Description is required")]
    [StringLength(ValidationConsts.MaxEntityDescriptionLength, MinimumLength = ValidationConsts.MinEntityDescriptionLength,
        ErrorMessage = "ProjectDescription must have between {2} and {1} digits")]
    public string ProjectDescription { get; set; } = string.Empty!.Trim();

    //-------------------INITIALDATE-------------------
    [Required(ErrorMessage = "Initial Date is required")]
    [DataType(DataType.DateTime)]
    public DateTime? InitialDate { get; set; }

    //-------------------DEADLINE-------------------
    [DataType(DataType.DateTime)]
    public DateTime? DeadLine { get; set; }
}

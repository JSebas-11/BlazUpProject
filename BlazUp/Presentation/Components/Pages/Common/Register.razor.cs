using Domain.Abstractions.Services.Facade;
using Domain.Common;
using Domain.Common.Enums;
using Domain.Models.Lookups;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Models.ModelForms;
using Presentation.Routing;

namespace Presentation.Components.Pages.Common;

public partial class Register {
    //--------------------------INITIALIZATIION--------------------------
    [Inject] public NavigationManager Nav { get; private set; } = default!;
    [Inject] public IDataService DataService { get; private set; } = default!;
    [Inject] public ISnackbar Snackbar { get; private set; } = default!;

    //--------------------------auxFields--------------------------
    private bool _showPassword = false;
    private bool _isLoading = false;
    private InputType _passwordIT => _showPassword ? InputType.Text : InputType.Password;

    private IReadOnlyList<UserRole>? _roles;
    private MudForm? _form;
    private readonly UserForm _userForm = new();

    //--------------------------METHODS--------------------------
    protected override async Task OnInitializedAsync() {
        try {
            _roles = await DataService.Catalogs.GetRolesAsync();
        }
        catch (Exception ex) {
            Snackbar.Add($"There has been an error loading UserRoles\n({ex.Message})", Severity.Error);
        }
    }

    private async Task Submit() {
        //Vinculada al boton disabled de botones (back/Submit)
        _isLoading = true;

        try {
            await _form!.Validate();

            if (!_form.IsValid) {
                Snackbar.Add("Inputs are not correct, take a look at them before Signing Up", Severity.Warning);
                return;
            }

            //Comprobar que no haya otro usuario con ese DNI (debe ser unique)
            if (await DataService.Users.ExistsUserAsync(_userForm.Dni)) {
                Snackbar.Add("There is already a user registered with that DNI", Severity.Warning);
                return;
            }

            //Crear usuario y mostrar los resultados correspondientes
            Result creationResult = await DataService.Users.CreateUserAsync(
                _userForm.Dni, _userForm.Password, _userForm.UserName, (Role)_userForm.RoleId
            );

            Snackbar.Add(creationResult.Description, creationResult.Success ? Severity.Success : Severity.Error);

            if (creationResult.Success)
                CleanInputs();
        }
        finally { _isLoading = false; }
    }

    private void NavigateToLogin() => Nav.NavigateTo(AppRoutes.Login);
    
    //--------------------------auxMeths--------------------------
    private void CleanInputs() {
        _userForm.Dni = string.Empty;
        _userForm.Password = string.Empty;
        _userForm.UserName = string.Empty;
        _userForm.RoleId = 0;
    }
}

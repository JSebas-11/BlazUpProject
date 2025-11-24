using Domain.Abstractions.Services.Facade;
using Domain.Common.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Components.ModelForms;
using Presentation.Routing;

namespace Presentation.Components.Pages.Common;

public partial class Login {
    //--------------------------INITIALIZATIION--------------------------
    [Inject] public NavigationManager Nav { get; private set; } = default!;
    [Inject] public IDataService DataService { get; private set; } = default!;
    [Inject] public ISnackbar Snackbar { get; private set; } = default!;

    //--------------------------auxFields--------------------------
    private bool _showPassword = false;
    private bool _isLoading = false; //Vinculada al boton disabled de botones (signUp/logIn)
    private InputType _passwordIT => _showPassword ? InputType.Text : InputType.Password;

    private MudForm? _form;
    private readonly UserForm _userForm = new();

    //--------------------------METHODS--------------------------
    private async Task Submit() {
        _isLoading = true;

        try {
            await _form!.Validate();

            if (!_form.IsValid) {
                Snackbar.Add("Inputs are not correct, take a look at them before Log In", Severity.Warning);
                return;
            }

            //Verificar que usuario exista y contraseña sea correcta
            UserInfo? user = await DataService.Users.GetUserAsync(_userForm.Dni, _userForm.Password);

            if (user is null) {
                Snackbar.Add("DNI and password do not match. Try again", Severity.Warning);
                return;
            }
            
            //En caso de logearse correctamente lo enviamos al index del layout que corresponda con su rol
            string layout = string.Empty;

            switch ((Role)user.RoleId) {
                case Role.Admin:
                    layout = AppRoutes.AdminIndex;
                    break;
                case Role.Client:
                    layout = AppRoutes.ClientIndex;
                    break;
                case Role.Developer:
                    layout = AppRoutes.DeveloperIndex;
                    break;
                default: throw new InvalidOperationException("User Role is not identified");
            }

            Nav.NavigateTo(layout);
        }
        finally { _isLoading = false; }
    }
    
    private void NavigateToRegister() => Nav.NavigateTo(AppRoutes.Register);
}

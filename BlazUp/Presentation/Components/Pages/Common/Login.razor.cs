using Domain.Abstractions.Services.Facade;
using Domain.Common.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Models.ModelForms;
using Presentation.Routing;
using Presentation.Services;

namespace Presentation.Components.Pages.Common;

public partial class Login {
    //--------------------------INITIALIZATIION--------------------------
    [Inject] public NavigationManager Nav { get; private set; } = default!;
    [Inject] public IDataService DataService { get; private set; } = default!;
    [Inject] public ISnackbar Snackbar { get; private set; } = default!;
    [Inject] public UserContext UserContext { get; private set; } = default!;

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

            //Creacion del UserContext (es usado para autenticacion y navegacion entre los layouts de cada rol)
            UserContext.SetUser(user);

            if (!UserContext.IsAuthenticated) throw new InvalidOperationException("User Role is not identified");

            Nav.NavigateTo(AppRoutes.Home);
        }
        finally { _isLoading = false; }
    }
    
    private void NavigateToRegister() => Nav.NavigateTo(AppRoutes.Register);
}

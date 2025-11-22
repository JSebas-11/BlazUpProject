using Domain.Abstractions.Services.Facade;
using Domain.Models.Lookups;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Components.ModelForms;
using Presentation.Routing;

namespace Presentation.Components.Pages.Common;

public partial class Register {
    //--------------------------INITIALIZATIION--------------------------
    [Inject] public NavigationManager Nav { get; private set; } = default!;
    [Inject] public IDataService DataService { get; private set; } = default!;
    [Inject] public ISnackbar Snackbar { get; private set; } = default!;

    //--------------------------auxFields--------------------------
    private bool _showPassword = false;
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
        await _form!.Validate();

        if (!_form.IsValid) {
            Snackbar.Add("Inputs are not correct, take a look at them before Signing Up", Severity.Warning);
            return;
        }
            
        Snackbar.Add($"Inputs Correct", Severity.Success);
    }

    private void NavigateToLogin() => Nav.NavigateTo(AppRoutes.Login);
}

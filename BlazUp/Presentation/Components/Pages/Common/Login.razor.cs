using Microsoft.AspNetCore.Components;
using Presentation.Routing;

namespace Presentation.Components.Pages.Common;

public partial class Login {
    //--------------------------INITIALIZATIION--------------------------
    [Inject] public NavigationManager Nav { get; private set; } = default!;


    //--------------------------METHODS--------------------------
    private void NavigateToRegister() => Nav.NavigateTo(AppRoutes.Register);
}

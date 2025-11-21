using Microsoft.AspNetCore.Components;
using Presentation.Routing;

namespace Presentation.Components.Pages.Common;

public partial class Register {
    //--------------------------INITIALIZATIION--------------------------
    [Inject] public NavigationManager Nav { get; private set; } = default!;


    //--------------------------METHODS--------------------------
    private void NavigateToLogin() => Nav.NavigateTo(AppRoutes.Login);
}

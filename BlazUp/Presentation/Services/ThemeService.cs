using MudBlazor;

namespace Presentation.Services;

//Clase para centralizar la creacion e interaccion de las paletas de colores, tipografias, etc
internal class ThemeService {
    //------------------------PROPERTIES------------------------
    public MudTheme LightTheme { get; }
    public MudTheme DarkTheme { get; }
    public MudTheme CurrentTheme { get; private set; }

    public event Action OnThemeChanged;

    //------------------------INITIALIZATION------------------------
    public ThemeService() {
        //------------------------LIGHT THEME------------------------
        LightTheme = new MudTheme();

        //------------------------DARK THEME------------------------
        DarkTheme = new MudTheme { PaletteDark = new PaletteDark() };

        CurrentTheme = DarkTheme;
    }

    //------------------------METHODS------------------------
    public void ToggleTheme() {
        CurrentTheme = CurrentTheme == LightTheme ? DarkTheme : LightTheme;
        OnThemeChanged?.Invoke();
    }
}
using Domain.Abstractions.Services.Facade;
using Domain.Common.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Components.Forms;
using Presentation.Mappers;
using Presentation.Models.Dtos;
using Presentation.Services;

namespace Presentation.Components.Pages.Common;

public partial class Projects {
    //--------------------------INITIALIZATIION--------------------------
    [Inject] public IDataService DataService { get; private set; } = default!;
    [Inject] public ISnackbar Snackbar { get; private set; } = default!;
    [Inject] public IDialogService DialogService { get; private set; } = default!;
    [Inject] public UserContext UserContext { get; private set; } = default!;

    //--------------------------auxFields--------------------------
    private bool _isLoading = true;

    //List<ProjectDto> _projects = new List<ProjectDto>();

    //--------------------------METHODS--------------------------
    protected override Task OnInitializedAsync() => LoadProjectsAsync();
    private async Task LoadProjectsAsync() {
        _isLoading = true;
        try {
            //var userProjects = await DataService.Projects.GetByUserAsync(UserContext.UserDto.UserId);
            //_projects = [.. userProjects.Select(ProjectMapper.ToDto)];
        }
        catch (Exception ex) {
            Snackbar.Add($"There has been an error loading User's projects\n({ex.Message})", Severity.Error);
        }
        finally { _isLoading = false; }
    }

    private async Task OpenProjectForm() {
        var dialog = await DialogService.ShowAsync<ProjectFormDialog>();
        var result = await dialog.Result;

        if (!result.Canceled) await LoadProjectsAsync();
    }
}

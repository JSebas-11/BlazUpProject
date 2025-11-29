using Domain.Abstractions.Services.Facade;
using Domain.Common;
using Domain.Common.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Presentation.Mappers;
using Presentation.Models.Dtos;
using Presentation.Models.ModelForms;
using Presentation.Services;

namespace Presentation.Components.Forms;

public partial class ProjectFormDialog {
    //--------------------------INITIALIZATIION--------------------------
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;
    [Inject] public IDataService DataService { get; set; } = default!;
    [Inject] public UserContext UserContext { get; set; } = default!;
    
    //--------------------------auxFields--------------------------
    private List<UserDto> _users = new (); //Usuarios vinculados al MudSelect
    private List<UserDto> _membersSelected = new ();
    private int _membersAmount = 0;

    private bool _isLoading = false; //Vinculada al boton disabled de botones (Close/Create)

    private MudForm? _form;
    private readonly ProjectForm _projectForm = new();

    private MudAutocomplete<UserDto> _macMembers;
    //--------------------------METHODS--------------------------
    protected override async Task OnInitializedAsync() {
        try {
            var rawUsers = await DataService.Users.GetUsersAsync();
            _users = [.. rawUsers.Select(UserMapper.ToDto)];
        }
        catch (Exception ex) {
            Snackbar.Add($"There has been an error loading Users\n({ex.Message})", Severity.Error);
        }
    }

    #region MembersMethods
    private async Task OnMemberSelected(UserDto member) {
        try {
            if (member is null) return;
            
            if (_membersSelected.Any(m => m.UserId == member.UserId)) return;
            
            _membersSelected.Add(member);
            _membersAmount++;
            StateHasChanged();
        }
        finally { await _macMembers.ClearAsync(); }
    }
    private void MemberOut(MudChip<UserDto> member) {
        _membersSelected.Remove((UserDto)member.Tag);
        _membersAmount--;
    }

    private Task<IEnumerable<UserDto>> UserSearch(string value, CancellationToken token) {
        if (string.IsNullOrWhiteSpace(value))
            return Task.FromResult(_users.AsEnumerable());
        
        string valueStr = value.Trim();

        //Busqueda por numero de DNI o UserName
        return Task.FromResult(
            _users.Where(u => u.UserDni.Contains(valueStr, StringComparison.OrdinalIgnoreCase) 
                    || u.UserName.Contains(valueStr, StringComparison.OrdinalIgnoreCase))
                .AsEnumerable()
        );
    }
    #endregion

    private void CleanFields() {
        _projectForm.ProjectName = string.Empty;
        _projectForm.ProjectDescription = string.Empty;
        _projectForm.InitialDate = null;
        _projectForm.DeadLine = null;

        _membersAmount = 0;
        _membersSelected.Clear();
    }
    private void Close() => MudDialog.Cancel();
    private async Task CreateForm() {
        _isLoading = true;
        try {
            await _form!.Validate();

            if (!_form.IsValid) {
                Snackbar.Add("Inputs are not correct, take a look at them before creating new project", Severity.Warning);
                return;
            }

            bool withMembers = _membersSelected.Count > 0;

            int creatorId = UserContext.UserDto.UserId;
            Result operation;
            if (withMembers) {
                IEnumerable<int> members = _membersSelected.Select(m => m.UserId);
                operation = await DataService.Projects.CreateWithMembersAsync(
                        _projectForm.ProjectName, _projectForm.ProjectDescription, _projectForm.InitialDate, StateEntity.Pending, creatorId, _projectForm.DeadLine, members
                    );
            } else {
                operation = await DataService.Projects.CreateAsync(
                        _projectForm.ProjectName, _projectForm.ProjectDescription, _projectForm.InitialDate, StateEntity.Pending, creatorId, _projectForm.DeadLine
                    );
            }

            if (!operation.Success) {
                Snackbar.Add(operation.Description, Severity.Warning);
                return;
            }

            Snackbar.Add(operation.Description, Severity.Success);
            CleanFields();
            MudDialog.Close();
        }
        finally { _isLoading = false; }
    }
}

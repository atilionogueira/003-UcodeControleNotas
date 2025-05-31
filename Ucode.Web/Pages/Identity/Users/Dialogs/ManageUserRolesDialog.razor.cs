using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.UserRoles;

namespace Ucode.Web.Pages.Identity.Users.Dialogs;

public partial class ManageUserRolesDialog
{
    [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;

    private List<RoleAssignmentResponse> Roles { get; set; } = new();
    private bool IsLoading { get; set; } = true;
    private bool _isSaving = false;
    private string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadRolesAsync();
    }

    private async Task LoadRolesAsync()
    {
        try
        {
            IsLoading = true;

            var client = HttpClientFactory.CreateClient(Configuration.HttpClientName);
            var url = $"v1/identity/admin/userroles/{UserId}";

            var response = await client.GetFromJsonAsync<Response<GetUserRolesResponse>>(url);

            if (response is null || response.Data is null)
            {
                ErrorMessage = "Erro ao carregar dados da API.";
                Snackbar.Add(ErrorMessage, Severity.Error);
                return;
            }

            Roles = response.Data.Roles;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erro ao carregar roles: {ex.Message}";
            Snackbar.Add(ErrorMessage, Severity.Error);
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected async Task SaveRolesAsync()
    {
        if (_isSaving)
            return;

        _isSaving = true;

        try
        {
            var client = HttpClientFactory.CreateClient(Configuration.HttpClientName);

            var request = new UpdateUserRolesRequest
            {
                TargetUserId = UserId,
                Roles = Roles.Where(x => x.IsAssigned).Select(x => x.RoleName).ToList()
            };

            var response = await client.PutAsJsonAsync($"v1/identity/admin/userroles/{UserId}", request);

            if (response.IsSuccessStatusCode)
            {
              //  Snackbar.Add("Roles atualizadas com sucesso!", Severity.Success);
                MudDialog.Close(DialogResult.Ok(true));
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Snackbar.Add($"Erro ao salvar roles: {error}", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Erro ao salvar roles: {ex.Message}", Severity.Error);
        }
        finally
        {
            _isSaving = false;
        }
    }
}

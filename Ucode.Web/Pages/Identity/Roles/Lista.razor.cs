using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Responses.Account.Role;

namespace Ucode.Web.Pages.Identity.Roles
{
    public class ListaRolePage : ComponentBase
    {
        #region Properties
        public bool isBusy { get; set; } = false;
        public List<RoleResponse> Roles { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IDialogService DialogService { get; set; } = null!;
        [Inject]
        public IRoleHandler Handler { get; set; } = null!;
        #endregion

        #region Override
        protected override async Task OnInitializedAsync() 
        {
            isBusy = true;
            try
            {
                var request = new GetAllRoleRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                    Roles = result.Data ?? [];
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally 
            {
                isBusy = false;
            }
        }
        #endregion

        #region Methods

        public async void OnDeleteButtonClickedAsync(long id, string name)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Ao prosseguir o lançamento {name} será excluído. Esta ação é irreversível! Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, name);
            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id, string name)
        {
            try
            {
                var request = new DeleteRoleRequest { Id = id };
                await Handler.DeleteAsync(request);
                Roles.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Role {name} excluída", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public Func<RoleResponse, bool> Filter => role =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
            if (role.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            if (role.Name?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            return false;
        };
        #endregion
    }
}

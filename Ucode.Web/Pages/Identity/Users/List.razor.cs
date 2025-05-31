using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses.Account.User;
using Ucode.Web.Pages.Identity.Users.Dialogs;

namespace Ucode.Web.Pages.Identity.Users
{
    public partial class ListUserPage : ComponentBase
    {

        #region Properties
        public bool IsBusy { get; set; } = false;
        public List<UserResponse> Users { get; set; } = [];
        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services       
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IDialogService DialogService { get; set; } = null!;
        [Inject]
        public IUserHandler Handler { get; set; } = null!;

        #endregion

        #region Override

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllUsersRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                    Users = result.Data ?? [];

            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
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
                var request = new DeleteUserRequest { Id = id };
                await Handler.DeleteAsync(request);
                Users.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Usuário{name} excluído", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public async Task OpenManageRolesDialogAsync(long userId, string userName)
        {
            var parameters = new DialogParameters
            {
                { "UserId", userId },
               { "UserName", userName }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true };

            var dialog = DialogService.Show<ManageUserRolesDialog>("Gerenciar Roles", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                Snackbar.Add("Roles atualizadas com sucesso.", Severity.Success);
                // Opcional: recarregar lista de usuários, se necessário
                //   await LoadUsersAsync(); // se você tiver esse método
            }
        }

        public Func<UserResponse, bool> Filter => user =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;
            if (user.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;
            if (user.UserName?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) == true)
                return true;
            if (user.Email?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            return false;
        };

    }
    #endregion
}

using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Requests.Account.User;

namespace Ucode.Web.Pages.Identity.Roles
{
    public class EditRolePage: ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateRoleRequest InputModel { get; set; } = new();

        #endregion

        #region Parameters

        [Parameter]
        public string Id { get; set; } = string.Empty;
        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public IRoleHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            GetRoleByIdRequest? request = null;
            try
            {
                request = new GetRoleByIdRequest
                {
                    Id = long.Parse(Id)
                };
            }
            catch
            {
                Snackbar.Add("Parâmetro inválido.", Severity.Error);
                return;
            }

            if (request is null)
                return;

            IsBusy = true;
            try
            {
                await Task.Delay(3000);
                var response = await Handler.GetByIdAsync(request);

                if (response.IsSuccess && response.Data is not null)
                    InputModel = new UpdateRoleRequest
                    {
                        Id = response.Data.Id,
                        Name = response.Data.Name                      
                    };
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

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add("Role atualizado", Severity.Success);
                    NavigationManager.NavigateTo("/admin/role");
                }
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
    }
}

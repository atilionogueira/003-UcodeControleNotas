using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;

namespace Ucode.Web.Pages.Identity.Roles
{
    public partial class CreateRolePage : ComponentBase
    {
        #region Properties
        public bool isBusy { get; set; } = false;
        public CreateRoleRequest InputModel { get; set; } = new();
        #endregion

        #region Services
        [Inject]
        public IRoleHandler Handler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region Methods

        public async Task OnValidSubmitAsync() 
        {
            isBusy = true;
            try
            {
                var result = await Handler.CreateAsync(InputModel);
                if (result.IsSuccess) 
                {
                    Snackbar.Add(result.Message, Severity.Error);
                    NavigationManager.NavigateTo("/admin/role");
                }
                else 
                {
                    Snackbar.Add(result.Message, Severity.Error);
                }
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
    }
}

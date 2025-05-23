using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;


namespace Ucode.Web.Pages.Identity.Users
{
    public partial class CreateUserPage : ComponentBase
    {
        #region Properties
        public bool isBusy { get; set; } = false;
        public CreateUserRequest InputModel { get; set; } = new();
        #endregion

        #region Services
        [Inject]
        public IUserHandler Handler { get; set; } = null!;
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
                    NavigationManager.NavigateTo("/admin/user");
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

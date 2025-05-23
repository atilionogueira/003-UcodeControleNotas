using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;


namespace Ucode.Web.Pages.Identity.Users
{
    public class EditUserPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateUserRequest InputModel { get; set; } = new();

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
        public IUserHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            GetUserByIdRequest? request = null;
            try
            {
                request = new GetUserByIdRequest
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
                    InputModel = new UpdateUserRequest
                    {
                        Id = response.Data.Id,
                        UserName = response.Data.UserName,
                        Email = response.Data.Email,
                        PhoneNumber = response.Data.PhoneNumber
                     
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
                    Snackbar.Add("Usuário atualizado", Severity.Success);
                    NavigationManager.NavigateTo("/admin/user");
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

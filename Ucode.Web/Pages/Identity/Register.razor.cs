﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account;
using Ucode.Web.Security;


namespace Ucode.Web.Pages.Identity
{
    public partial class RegisterPage : ComponentBase
    {
        #region Dependencies
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IAccountHandler Handler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        #endregion

        #region Properties
        public bool IsBusy { get; set; } = false;
        public RegisterRequest InputModel { get; set; } = new();
        #endregion

        #region
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;


            //if (user.Identity is not null && user.Identity.IsAuthenticated)
            if (user.Identity is { IsAuthenticated: true })
                NavigationManager.NavigateTo("/");
        }
        #endregion

        #region Methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.RegisterAsync(InputModel);

                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo("/login");
                    Snackbar.Add(result.Message, Severity.Success);
                }
                else
                    Snackbar.Add(result.Message, Severity.Error); 
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

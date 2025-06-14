﻿using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.UserRoles;

namespace Ucode.Web.Handler.Identity
{
    public class UserRoleHandler(IHttpClientFactory httpClientFactory) : IUserRoleHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<UserRoleResponse>> CreateAsync(CreateUserRoleRequest request)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("v1/identity/admin/userroles", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new Response<UserRoleResponse>(null, (int)response.StatusCode, $"Erro ao atribuir role ao usuário: {errorMessage}");
                }

                var content = await response.Content.ReadFromJsonAsync<Response<UserRoleResponse>>();
                return content ?? new Response<UserRoleResponse>(null, 400, "Resposta inválida do servidor");
            }
            catch (Exception ex)
            {
                return new Response<UserRoleResponse>(null, 500, $"Erro inesperado: {ex.Message}");
            }
        }

        public async Task<Response<GetUserRolesResponse>> GetUserRolesAsync(GetUserRolesRequest request)
        {
            var response = await _client.GetAsync($"v1/identity/admin/userroles/{request.TargetUserId}");

            if (!response.IsSuccessStatusCode)
                return new Response<GetUserRolesResponse>(null, (int)response.StatusCode, "Erro ao buscar roles do usuário");

            var content = await response.Content.ReadFromJsonAsync<Response<GetUserRolesResponse>>();
            return content ?? new Response<GetUserRolesResponse>(null, 400, "Resposta inválida do servidor");
        }

        public async Task<Response<GetUserRolesResponse>> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            var response = await _client.PutAsJsonAsync("v1/identity/admin/userroles/{request.TargetUserId}", request);           

            if (!response.IsSuccessStatusCode)
                return new Response<GetUserRolesResponse>(null, (int)response.StatusCode, "Erro ao atualizar roles do usuário");

            var content = await response.Content.ReadFromJsonAsync<Response<GetUserRolesResponse>>();
            return content ?? new Response<GetUserRolesResponse>(null, 400, "Resposta inválida do servidor");
        }
    }
}

using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.UserRoles;

namespace Ucode.Web.Handler.Identity
{
    public class UserRoleHandler(IHttpClientFactory httpClientFactory) : IUserRoleHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
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

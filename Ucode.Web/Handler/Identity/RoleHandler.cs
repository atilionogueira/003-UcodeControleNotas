using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Role;
using Ucode.Core.Responses.Account.User;


namespace Ucode.Web.Handler.Identity
{
    public class RoleHandler(IHttpClientFactory httpClientFactory) : IRoleHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<PagedResponse<List<RoleResponse>>> GetAllAsync(GetAllRoleRequest request)
         => await _client.GetFromJsonAsync<PagedResponse<List<RoleResponse>>>("v1/identity/admin/role")
                ?? new PagedResponse<List<RoleResponse>>(null, 400, "Não foi possível obter os roles");

        public async Task<Response<RoleResponse>> GetByIdAsync(GetRoleByIdRequest request)
         => await _client.GetFromJsonAsync<Response<RoleResponse>>($"v1/Identity/admin/role/{request.Id}")
                ?? new Response<RoleResponse>(null, 400, "Não foi possível obter o role");
        public async Task<Response<RoleResponse>> CreateAsync(CreateRoleRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/admin/role", request);
            return await result.Content.ReadFromJsonAsync<Response<RoleResponse>>()
                ?? new Response<RoleResponse>(null, 400, "Falha ao criar o role");
        }
        public async Task<Response<RoleResponse>> UpdateAsync(UpdateRoleRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/identity/admin/role/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<RoleResponse>>()
                ?? new Response<RoleResponse>(null, 400, "Falha ao criar o role");
        }
        public async Task<Response<RoleResponse>> DeleteAsync(DeleteRoleRequest request)
        {
            var result = await _client.DeleteAsync($"v1/identity/admin/role/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<RoleResponse>>()
                ?? new Response<RoleResponse>(null, 400, "Falha ao excluir o usuário");
        }      

    }
}

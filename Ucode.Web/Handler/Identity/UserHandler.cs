using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Web.Handlers.Identity
{
    public class UserHandler(IHttpClientFactory httpClientFactory) : IUserHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<PagedResponse<List<UserResponse>>> GetAllAsync(GetAllUsersRequest request)
              => await _client.GetFromJsonAsync<PagedResponse<List<UserResponse>>>("v1/identity/admin/user")
                ?? new PagedResponse<List<UserResponse>>(null, 400, "Não foi possível obter os usuários");
        public async Task<Response<UserResponse>> GetByIdAsync(GetUserByIdRequest request)
          => await _client.GetFromJsonAsync<Response<UserResponse>>($"v1/Identity/admin/user/{request.Id}")
                ?? new Response<UserResponse>(null, 400, "Não foi possível obter o usuário");
        public async Task<Response<UserResponse>> CreateAsync(CreateUserRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/admin/user", request);
            return await result.Content.ReadFromJsonAsync<Response<UserResponse>>()
                ?? new Response<UserResponse>(null, 400, "Falha ao criar o usuário");
        }

        public async Task<Response<UserResponse>> UpdateAsync(UpdateUserRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/identity/admin/user/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<UserResponse>>()
                ?? new Response<UserResponse>(null, 400, "Falha ao atualizar o usuário");
        }

        public async Task<Response<UserResponse>> DeleteAsync(DeleteUserRequest request)
        {
            var result = await _client.DeleteAsync($"v1/identity/admin/user/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<UserResponse>>()
                ?? new Response<UserResponse>(null, 400, "Falha ao excluir o usuário");
        }

    }   

       
}

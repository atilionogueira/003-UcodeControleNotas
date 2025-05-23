using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Students;
using Ucode.Core.Responses;

namespace Ucode.Web.Handlers
{
    public class StudentHandler(IHttpClientFactory httpClientFactory) : IStudentHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<PagedResponse<List<Student>>> GetAllAsync(GetAllStudentRequest request)
             => await _client.GetFromJsonAsync<PagedResponse<List<Student>>>("v1/students")
             ?? new PagedResponse<List<Student>>(null, 400, "Não foi possível obter as categorias");


        public async Task<Response<Student?>> GetByIdAsync(GetStudentByIdRequest request)
            => await _client.GetFromJsonAsync<Response<Student?>>($"v1/students/{request.Id}")
            ?? new Response<Student?>(null, 400, "Não foi possível recuperar o estudante");

        public async Task<Response<Student?>> CreateAsync(CreateStudentRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/students", request);
            return await result.Content.ReadFromJsonAsync<Response<Student?>>()
                ?? new Response<Student?>(null, 400, "Falha ao criar o estudante");
        }

        public async Task<Response<Student?>> UpdateAsync(UpdateStudentRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/students/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Student?>>()
                ?? new Response<Student?>(null, 400, "Falha ao ataulizar o estudante");

        }

        public async Task<Response<Student?>> DeleteAsync(DeleteStudentRequest request)
        {
            var result = await _client.DeleteAsync($"v1/students/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Student?>>()
                ?? new Response<Student?>(null, 400, "Falha ao excluir o estudante");
        }

    }

}

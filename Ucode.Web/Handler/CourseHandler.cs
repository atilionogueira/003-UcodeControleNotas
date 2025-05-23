using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Course;

using Ucode.Core.Responses;

namespace Ucode.Web.Handlers
{
    public class CourseHandler(IHttpClientFactory httpClientFactory) : ICourseHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<PagedResponse<List<Course>>> GetAllAsync(GetAllCourseRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Course>>>("v1/courses")
                ?? new PagedResponse<List<Course>>(null, 400, "Não foi possível obter os cursos");

        public async Task<Response<Course?>> GetByIdAsync(GetCourseByRequest request)
            => await _client.GetFromJsonAsync<Response<Course?>>($"v1/courses/{request.Id}")
                ?? new Response<Course?>(null, 400, "Não foi possível obter o curso");

        public async Task<Response<Course?>> CreateAsync(CreateCourseRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/courses", request);
            return await result.Content.ReadFromJsonAsync<Response<Course?>>()
                ?? new Response<Course?>(null, 400, "Falha ao criar o Curso");

        }

        public async Task<Response<Course?>> UpdateAsync(UpdateCourseRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/courses/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Course?>>()
                ?? new Response<Course?>(null, 400, "Falha ao atualizar o curso");

        }

        public async Task<Response<Course?>> DeleteAsync(DeleteCourseRequest request)
        {
            var result = await _client.DeleteAsync($"v1/courses/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Course?>>()
                ?? new Response<Course?>(null, 400, "Falha ao excluir o estudante");
        }
    }
}

using System.Net.Http.Json;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Enrollment;

namespace Ucode.Web.Handler
{
    public class EnrollmentHandler(IHttpClientFactory httpClientFactory) : IEnrollmentHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<PagedResponse<List<EnrollmentListResponse>>> GetAllAsync(GetAllEnrollmentRequest request)
             => await _client.GetFromJsonAsync<PagedResponse<List<EnrollmentListResponse>>>("v1/enrollments")
             ?? new PagedResponse<List<EnrollmentListResponse>>(null, 400, "Não foi possível obter as matrícula");

        public async Task<Response<Enrollment?>> GetByIdAsync(GetEnrollmentsByIdRequest request)
            => await _client.GetFromJsonAsync<Response<Enrollment?>>($"v1/enrollments/{request.Id}")
            ?? new Response<Enrollment?>(null, 400, "Não foi possível recuperar a matrícula");

        public async Task<Response<Enrollment?>> CreateAsync(CreateEnrollmentRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/enrollments", request);
            return await result.Content.ReadFromJsonAsync<Response<Enrollment?>>()
                ?? new Response<Enrollment?>(null, 400, "Falha ao criar a matrícula");
        }

        public async Task<Response<Enrollment?>> UpdateAsync(UpdateEnrollmentRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/enrollments/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Enrollment?>>()
                ?? new Response<Enrollment?>(null, 400, "Falha ao ataulizar a matrícula");
        }

        public async Task<Response<Enrollment?>> DeleteAsync(DeleteEnrollmentsRequest request)
        {
            var result = await _client.DeleteAsync($"v1/enrollments/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Enrollment?>>()
                ?? new Response<Enrollment?>(null, 400, "Falha ao excluir a matrícula");
        }     
               
    }
}

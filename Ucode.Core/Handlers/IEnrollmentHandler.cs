using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Enrollment;


namespace Ucode.Core.Handlers
{
    public interface IEnrollmentHandler 
    {
        Task<Response<Enrollment?>> CreateAsync(CreateEnrollmentRequest request);
        Task<Response<Enrollment?>> UpdateAsync(UpdateEnrollmentRequest request);
        Task<Response<Enrollment?>> DeleteAsync(DeleteEnrollmentsRequest request);
        Task<Response<Enrollment?>> GetByIdAsync(GetEnrollmentsByIdRequest request);
        Task<PagedResponse<List<EnrollmentListResponse>>> GetAllAsync(GetAllEnrollmentRequest request);

    }
}

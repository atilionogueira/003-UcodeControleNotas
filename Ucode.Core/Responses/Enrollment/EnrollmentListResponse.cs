
using Ucode.Core.Emuns;

namespace Ucode.Core.Responses.Enrollment
{
    public class EnrollmentListResponse
    {
        public long Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public EEnrollmentStatus Status { get; set; }
    }
}

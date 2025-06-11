
using System.ComponentModel.DataAnnotations;
using Ucode.Core.Emuns;

namespace Ucode.Core.Requests.Enrollments
{
    public class UpdateEnrollmentRequest : Request
    {
        public long Id { get; set; }
        public string EnrollmentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status é obrigatório")]
        public EEnrollmentStatus Status { get; set; } = EEnrollmentStatus.Active;

        [Required(ErrorMessage = "Student inválido")]
        public long StudentId { get; set; }

        [Required(ErrorMessage = "Course inválido")]
        public long CourseId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? UpdatedAt { get; set; }

    }
}

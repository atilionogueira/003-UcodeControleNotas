
using System.ComponentModel.DataAnnotations;
using Ucode.Core.Emuns;

namespace Ucode.Core.Requests.Enrollments
{
    public class CreateEnrollmentRequest : Request 
    {     

        [Required(ErrorMessage = "Status é obrigatório")]
        public EEnrollmentStatus Status { get; set; } = EEnrollmentStatus.Active;

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Estudante inválido")]
        public long StudentId { get; set; }

        [Required(ErrorMessage = "Curso inválido")]
        public long CourseId { get; set; }
    }  
}

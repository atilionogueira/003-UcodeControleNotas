using System.Data;
using Ucode.Core.Emuns;

namespace Ucode.Core.Models
    {
        public class Enrollment
        {
            public long Id { get; set; }
            public string EnrollmentNumber { get; set; } = Guid.NewGuid().ToString("N")[..8];
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime? UpdatedAt { get; set; } 
            public long StudentId { get; set; }
            public Student Student { get; set; } = null!;
            public long CourseId { get; set; }
            public Course Course { get; set; } = null!;

            public EEnrollmentStatus Status { get; set; }
            public bool IsActive { get; set; }
            public string UserId { get; set; } = string.Empty;
        }
    }


﻿
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Course
{
    public class CreateCourseRequest : Request
    {
        [Required(ErrorMessage = "Nome inválido")]
        [MaxLength(100, ErrorMessage = "O nome deve conter até 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição inválido")]
        [MaxLength(100, ErrorMessage = "Descrição deve conter até 255 caracteres")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Duração do curso inválido")]
        public int DurationInHours { get; set; }       
     
    }
}

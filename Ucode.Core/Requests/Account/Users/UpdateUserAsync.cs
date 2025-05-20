
using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Account.User
{
    public class UpdateUserRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(180, ErrorMessage = "Máximo de 180 caracteres.")]
        public string UserName { get; set; } = string.Empty;


        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        [StringLength(180, ErrorMessage = "Máximo de 180 caracteres.")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme a senha.")]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; } = string.Empty;


        [Phone(ErrorMessage = "Número de telefone inválido.")]
        [StringLength(20, ErrorMessage = "Máximo de 20 caracteres.")]
        public string? PhoneNumber { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Account.Roles
{
    public class CreateRoleRequest : Request
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
        public string Name { get; set; } = string.Empty;
    }
}

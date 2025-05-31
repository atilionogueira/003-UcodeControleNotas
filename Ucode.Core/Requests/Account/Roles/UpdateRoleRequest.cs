using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Account.Roles
{
    public class UpdateRoleRequest : Request
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
        public string Name { get; set; } = string.Empty;
    }
}

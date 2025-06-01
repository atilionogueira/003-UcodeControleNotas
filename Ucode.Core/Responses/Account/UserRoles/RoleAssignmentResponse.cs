
namespace Ucode.Core.Responses.Account.UserRoles
{
    public class RoleAssignmentResponse
    {
        public long Id { get; set; } // IdentityUser.Id é long
        public string RoleName { get; set; } = string.Empty;
        public bool IsAssigned { get; set; } = false; // Atribuido
    }
}

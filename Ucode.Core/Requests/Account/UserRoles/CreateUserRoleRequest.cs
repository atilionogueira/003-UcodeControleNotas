using System.ComponentModel.DataAnnotations;

namespace Ucode.Core.Requests.Account.UserRoles
{
    public class CreateUserRoleRequest : Request
    {
        public long TargetUserId { get; set; }
        public long RoleId { get; set; }
    }
}

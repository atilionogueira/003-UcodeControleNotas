namespace Ucode.Core.Responses.Account.UserRoles
{
    public class UserRoleResponse
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool IsAssigned { get; set; }
    }
}

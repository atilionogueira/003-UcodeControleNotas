namespace Ucode.Core.Requests.Account.UserRoles
{
    public class UpdateUserRolesRequest : Request
    {
        public long TargetUserId { get; set; }         // o usuário que vai ter as roles alteradas
        public List<string> Roles { get; set; } = new();
    }
}

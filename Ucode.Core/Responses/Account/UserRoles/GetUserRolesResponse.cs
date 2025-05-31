namespace Ucode.Core.Responses.Account.UserRoles
{
    public class GetUserRolesResponse
    {
        public long Id { get; set; }
        public List<RoleAssignmentResponse> Roles { get; set; } = new();
    }
}

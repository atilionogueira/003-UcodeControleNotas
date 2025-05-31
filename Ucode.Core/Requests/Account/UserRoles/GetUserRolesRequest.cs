
namespace Ucode.Core.Requests.Account.UserRoles
{
    public class GetUserRolesRequest : Request
    {
        public long TargetUserId { get; set; }  // ID do usuário cujas roles queremos consultar
    }
}

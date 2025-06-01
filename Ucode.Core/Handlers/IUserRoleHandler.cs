using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses.Account.UserRoles;
using Ucode.Core.Responses;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Core.Handlers
{
    public interface IUserRoleHandler
    {

        Task<Response<UserRoleResponse>> CreateAsync(CreateUserRoleRequest request);
        
        Task<Response<GetUserRolesResponse>> GetUserRolesAsync(GetUserRolesRequest request);
     
        Task<Response<GetUserRolesResponse>> UpdateUserRolesAsync(UpdateUserRolesRequest request);

    }
}

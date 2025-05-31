using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Role;
using Ucode.Core.Requests.Account.Roles;

namespace Ucode.Core.Handlers
{
    public interface IRoleHandler
    {
        Task<Response<RoleResponse>> CreateAsync(CreateRoleRequest request);
        Task<Response<RoleResponse>> UpdateAsync(UpdateRoleRequest request);
        Task<Response<RoleResponse>> DeleteAsync(DeleteRoleRequest request);
        Task<Response<RoleResponse>> GetByIdAsync(GetRoleByIdRequest request);
        Task<PagedResponse<List<RoleResponse>>> GetAllAsync(GetAllRoleRequest request);
    }
}

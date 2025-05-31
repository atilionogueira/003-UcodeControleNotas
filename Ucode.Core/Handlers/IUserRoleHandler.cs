using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses.Account.UserRoles;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IUserRoleHandler
    {
        // Retorna as roles disponíveis e quais estão atribuídas a um usuário
        Task<Response<GetUserRolesResponse>> GetUserRolesAsync(GetUserRolesRequest request);
        // Atualiza as roles atribuídas a um usuário.
        Task<Response<GetUserRolesResponse>> UpdateUserRolesAsync(UpdateUserRolesRequest request);

    }
}

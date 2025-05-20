
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses.Account.User;
using Ucode.Core.Responses;

namespace Ucode.Core.Handlers
{
    public interface IUserHandler
    {
        Task<Response<UserResponse>> CreateAsync(CreateUserRequest request);
        Task<Response<UserResponse>> UpdateAsync(UpdateUserRequest request);
        Task<Response<UserResponse>> DeleteAsync(DeleteUserRequest request);
        Task<Response<UserResponse>> GetByIdAsync(GetUserByIdRequest request);
        Task<PagedResponse<List<UserResponse>>> GetAllAsync(GetAllUsersRequest request);
    }
}

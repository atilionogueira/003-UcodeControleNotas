using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.UsersEndpoints
{
    public class DeleteUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/admin/user/{id}", HandlerAsync)
            .WithName("Delete: User")
            .WithSummary("Delete a user")
            .WithDescription("Delete a user by Id")
            .WithOrder(3)
            .Produces<Response<UserResponse>>();

        private static async Task<IResult> HandlerAsync(
            IUserHandler handler,
            long id)
        {
            var request = new DeleteUserRequest
            {
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

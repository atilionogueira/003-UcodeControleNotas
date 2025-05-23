using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.UsersEndpoints
{
    public class UpdateUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("admin/user/{id:long}", HandlerAsync)
            .WithName("Update: User")
            .WithSummary("Updates an existing user")
            .WithDescription("Updates the details of an existing user by Id")
            .WithOrder(4)
            .Produces<Response<UserResponse>>();

        private static async Task<IResult> HandlerAsync(
            IUserHandler handler,
            long id,
            UpdateUserRequest request)
        {
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

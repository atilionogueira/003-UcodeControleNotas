using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.UsersEndpoints
{
    public class GetAllUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/admin/user", HandlerAsync)
            .WithName("Get: All Users")
            .WithSummary("Returns all users")
            .WithDescription("Returns all users from the system")
            .WithOrder(1)
            .Produces<Response<List<UserResponse>>>();

        private static async Task<IResult> HandlerAsync(
            IUserHandler handler)
        {
            var request = new GetAllUsersRequest();
            var result = await handler.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

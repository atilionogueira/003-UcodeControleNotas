using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.UsersEndpoints
{
    public class GetUserByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/users/{id:long}", HandlerAsync)
            .WithName("Get: User By Id")
            .WithSummary("Returns a user by Id")
            .WithDescription("Returns the user details for the given Id")
            .WithOrder(2)
            .Produces<Response<UserResponse>>();

        private static async Task<IResult> HandlerAsync(
            IUserHandler handler,
            long id)
        {
            var request = new GetUserByIdRequest { Id = id };
            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.NotFound(result);
        }
    }
}

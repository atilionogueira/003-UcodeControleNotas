using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.RolesEndpoints
{
    public class GetAllRoleEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/admin/role", HandlerAsync)
            .WithName("Get: All Roles")
            .WithSummary("Returns all roles")
            .WithDescription("Returns all roles from the system")
            .WithOrder(1)
            .Produces<Response<List<UserResponse>>>();

        private static async Task<IResult> HandlerAsync(
            IRoleHandler handler)
        {
            var request = new GetAllRoleRequest();
            var result = await handler.GetAllAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

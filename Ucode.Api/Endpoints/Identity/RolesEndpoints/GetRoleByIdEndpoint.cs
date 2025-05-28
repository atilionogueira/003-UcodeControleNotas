using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.RolesEndpoints
{
    public class GetRoleByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
          => app.MapGet("/admin/role/{id:long}", HandlerAsync)
            .WithName("Get: Role By Id")
            .WithSummary("Returns a role by Id")
            .WithDescription("Returns the role details for the given Id")
            .WithOrder(2)
            .Produces<Response<UserResponse>>();

        private static async Task<IResult> HandlerAsync(
            IRoleHandler handler,
            long id)
        {
            var request = new GetRoleByIdRequest { Id = id };
            var result = await handler.GetByIdAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.NotFound(result);
        }
    }
}

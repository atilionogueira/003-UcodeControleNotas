using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.RolesEndpoints
{
    public class DeleteRoleEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapDelete("/admin/role/{id}", HandlerAsync)
            .WithName("Delete: Role")
            .WithSummary("Delete a role")
            .WithDescription("Delete a role by Id")
            .WithOrder(3)
            .Produces<Response<UserResponse>>();

        private static async Task<IResult> HandlerAsync(
            IRoleHandler handler,
            long id)
        {
            var request = new DeleteRoleRequest
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

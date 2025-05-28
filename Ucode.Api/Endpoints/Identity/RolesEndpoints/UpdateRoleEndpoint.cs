using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.Role;


namespace Ucode.Api.Endpoints.Identity.RolesEndpoints
{
    public class UpdateRoleEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
          => app.MapPut("admin/role/{id:long}", HandlerAsync)
            .WithName("Update: Role")
            .WithSummary("Updates an existing role")
            .WithDescription("Updates the details of an existing role by Id")
            .WithOrder(4)
            .Produces<Response<RoleResponse>>();

        private static async Task<IResult> HandlerAsync(
            IRoleHandler handler,
            long id,
            UpdateRoleRequest request)
        {
            request.Id = id;

            var result = await handler.UpdateAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

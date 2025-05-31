using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.UserRoles;


namespace Ucode.Api.Endpoints.Identity.UserRolesEndpoints
{
    public class UpdateUserRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapPut("/admin/userroles/{id:long}", HandlerAsync)
                  .WithName("Put: Update User Roles")
                  .WithSummary("Updates roles assigned to a specific user")
                  .WithDescription("Removes all current roles and assigns new ones to the user")
                  .WithOrder(2)
                  .Produces<Response<GetUserRolesResponse>>();
                //  .RequireAuthorization(); // Proteção opcional

        private static async Task<IResult> HandlerAsync(
            UpdateUserRolesRequest request,
            long Id,
            IUserRoleHandler handler)
        {
            var result = await handler.UpdateUserRolesAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

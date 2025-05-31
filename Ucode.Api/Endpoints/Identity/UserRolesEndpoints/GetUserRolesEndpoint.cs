using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.UserRoles;

namespace Ucode.Api.Endpoints.Identity.UserRolesEndpoints
{
    public class GetUserRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/admin/userroles/{Id:long}", HandlerAsync)
                  .WithName("Get: User Roles")
                  .WithSummary("Returns the roles assigned and available for a user")
                  .WithDescription("Returns all roles with flags indicating if the user has each role")
                  .WithOrder(1)
                  .Produces<Response<GetUserRolesResponse>>();
              

        private static async Task<IResult> HandlerAsync(
            long Id,
            IUserRoleHandler handler)
        {
            var request = new GetUserRolesRequest { TargetUserId = Id };
            var result = await handler.GetUserRolesAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.UserRoles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.UserRoles;

namespace Ucode.Api.Endpoints.Identity.UserRolesEndpoints
{
    public class CreateUserRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapPost("/admin/userroles", HandlerAsync)
                   .WithName("Post: Create User Role")
                   .WithSummary("Associa uma role a um usuário")
                   .WithDescription("Associa uma única role a um usuário informado via UserId e RoleId")
                   .WithOrder(1)
                   .Produces<Response<UserRoleResponse>>();
        private static async Task<IResult> HandlerAsync(
           CreateUserRoleRequest request,
           IUserRoleHandler handler)
        {
            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}

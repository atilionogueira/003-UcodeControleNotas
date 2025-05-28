using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.Roles;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.RolesEndpoints
{
    public class CreateRoleEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/admin/role", HandleAsync)
           .WithName("Role: Create")
           .WithSummary("Create new role")
           .WithDescription("Roles a new user in the system")
           .WithOrder(1)
           .Produces<Response<UserResponse>>();


        private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IRoleHandler handler,
        CreateRoleRequest request)
        {
            // Exemplo: se quiser setar userId do criador para auditoria (opcional)
            var UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            // request.CreatedBy = creatorUserId; // Criar essa propriedade se quiser usar

            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/roles/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}


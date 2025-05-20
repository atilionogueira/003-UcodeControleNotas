using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account.User;
using Ucode.Core.Responses;
using Ucode.Core.Responses.Account.User;

namespace Ucode.Api.Endpoints.Identity.UsersEndpoints
{
    public class CreateUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
          => app.MapPost("/user", HandleAsync)
           .WithName("User: Create")
           .WithSummary("Create new User")
           .WithDescription("Creates a new user in the system")
           .WithOrder(1)
           .Produces<Response<UserResponse>>();


        private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUserHandler handler,
        CreateUserRequest request)
        {
            // Exemplo: se quiser setar userId do criador para auditoria (opcional)
            var UserId = user.Identity?.Name ?? string.Empty;
            // request.CreatedBy = creatorUserId; // Criar essa propriedade se quiser usar

            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"/users/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}

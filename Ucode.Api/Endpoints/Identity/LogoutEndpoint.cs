using Microsoft.AspNetCore.Identity;
using Ucode.Api.Common.Api;



namespace Ucode.Api.Endpoints.Identity
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
              => app
            .MapPost("/logout", HandleAsync)
            .RequireAuthorization();

        private static async Task<IResult> HandleAsync(SignInManager<Ucode.Api.Models.User> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
    }
}

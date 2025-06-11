using System.Security.Claims;
using Ucode.Api.Common.Api;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Enrollments;
using Ucode.Core.Responses;

namespace Ucode.Api.Endpoints.Enrollments
{
    public class CreateEnrollmentEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandlerAsync)
            .WithName("Enrollment: Create")
            .WithSummary("Create New Enrollment")
            .WithDescription("Create New Enrollment")
            .WithOrder(1)
            .Produces<Response<Enrollment?>>();

        private static async Task<IResult>HandlerAsync(
            ClaimsPrincipal user,
            IEnrollmentHandler handler,
            CreateEnrollmentRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;

            var result = await handler.CreateAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }            
    }
}

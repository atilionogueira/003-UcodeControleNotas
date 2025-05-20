using Ucode.Api.Common.Api;
using Ucode.Api.Endpoints.Courses;
using Ucode.Api.Endpoints.Enrollments;
using Ucode.Api.Endpoints.Grades;
using Ucode.Api.Endpoints.Identity;
using Ucode.Api.Endpoints.Identity.UsersEndpoints;
using Ucode.Api.Endpoints.Students;
using Ucode.Api.Models;



namespace Ucode.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app
                .MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "OK" });


            endpoints.MapGroup("v1/students")
                .WithTags("Students")
                .RequireAuthorization()
                .MapEndpoint<CreateStudentEnpoint>()
                .MapEndpoint<UpdateStudentEndpoint>()
                .MapEndpoint<DeleteStudentEndpoint>()
                .MapEndpoint<GetStudentByIdEndpoint>()
                .MapEndpoint<GetAllStudentEndpoint>();

            endpoints.MapGroup("v1/courses")
               .WithTags("Course")
               .RequireAuthorization()
               .MapEndpoint<CreateCourseEndpoint>()
               .MapEndpoint<UpdateCourseEndpoint>()
               .MapEndpoint<DeleteCourseEndpoint>()
               .MapEndpoint<GetCourseByIdEndpoint>()
               .MapEndpoint<GetAllCourseEndpoint>();

            endpoints.MapGroup("v1/grades")
              .WithTags("Grade")
              .RequireAuthorization()
              .MapEndpoint<CreateGradeEndpoint>()
              .MapEndpoint<UpdateGradeEndpoint>()
              .MapEndpoint<DeleteGradeEndpoint>()
              .MapEndpoint<GetGradeByIdEndpoint>()
              .MapEndpoint<GetAllGradeEndpoint>();

            endpoints.MapGroup("v1/Enrollment")
             .WithTags("Enrollment")
             .RequireAuthorization()
             .MapEndpoint<CreateEnrollmentEndpoint>()
             .MapEndpoint<UpdateEnrollmentEndpoint>()
             .MapEndpoint<DeleteEnrollmentsEndpoint>()
             .MapEndpoint<GetEnrollmentByIdEndpoint>()
             .MapEndpoint<GetAllEnrollmentEndpoint>();

            endpoints.MapGroup("v1/identity")
             .WithTags("Identity")
             .MapIdentityApi<Ucode.Api.Models.User>();


            endpoints.MapGroup("v1/identity")
             .WithTags("Identity")
             .MapEndpoint<LogoutEndpoint>()
             .MapEndpoint<GetRolesEndpoint>()
             .MapEndpoint<CreateUserEndpoint>()
             .MapEndpoint<DeleteUserEndpoint>()
             .MapEndpoint<GetAllUserEndpoint>()
             .MapEndpoint<GetUserByIdEndpoint>()
             .MapEndpoint<UpdateUserEndpoint>();              

        }
        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
    


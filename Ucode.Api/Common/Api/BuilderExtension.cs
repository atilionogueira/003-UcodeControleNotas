using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Api.Handlers;
using Ucode.Api.Handlers.Identity;
using Ucode.Api.Models;
using Ucode.Core;
using Ucode.Core.Handlers;

namespace Ucode.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        // Configurações para o Swagger
        public static void AddDocumentation( this WebApplicationBuilder builder) 
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });   

            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? String.Empty;
            Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? String.Empty;
        }

        //Registra o Identiy

        public static void AddSecurity(this WebApplicationBuilder builder) 
        {

            builder.Services
                .AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddIdentityCookies();
            builder.Services.AddAuthorization();
        }

        // Configuraçao para acessar o Banco 
        public static void AddDataContexts(this WebApplicationBuilder builder) 
        {
            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(Configuration.ConnectionString);
            });

            builder.Services
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddApiEndpoints();
        }

        // AddCors
        public static void AddCrossOrigin(this WebApplicationBuilder builder) 
        {
            builder.Services.AddCors(otpions => otpions.AddPolicy(
                ApiConfiguration.CorsPolicyName,  policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                ));
        }

        // Registra o handler como um serviço transiente
        public static void AddServices(this WebApplicationBuilder builder) 
        {
            builder.Services.AddTransient<IStudentHandler, StudentHandler>();
            builder.Services.AddTransient<ICourseHandler, CourseHandler>();
            builder.Services.AddTransient<IGradeHandler, GradeHandler>();
            builder.Services.AddTransient<IEnrollmentHandler, EnrollmentHandler>();
            builder.Services.AddTransient<IUserHandler, UserHandler>();
        }
    }
}

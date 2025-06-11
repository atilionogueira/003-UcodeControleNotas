using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Ucode.Core.Handlers;
using Ucode.Web;
using Ucode.Web.Handler;
using Ucode.Web.Handler.Identity;
using Ucode.Web.Handlers;
using Ucode.Web.Handlers.Identity;
using Ucode.Web.Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty; 

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddAuthorizationCore();


builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x =>
    (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName,opt => 
{
    opt.BaseAddress = new Uri(Configuration.BackendUrl);
})
    .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<IStudentHandler,StudentHandler>();
builder.Services.AddTransient<ICourseHandler, CourseHandler>();
builder.Services.AddTransient<IUserHandler, UserHandler>();
builder.Services.AddTransient<IRoleHandler, RoleHandler>();
builder.Services.AddTransient<IUserRoleHandler,UserRoleHandler>();
builder.Services.AddTransient<IEnrollmentHandler, EnrollmentHandler>();



await builder.Build().RunAsync();

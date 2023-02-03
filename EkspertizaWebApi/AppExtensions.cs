using EkspertizaWebApi.Services;
using EkspertizaWebApiData;
using EkspertizaWebApiData.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EkspertizaWebApi;

public static class AppExtensions
{
    public static WebApplication UseEkspertizaWebApi(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        context.Database.Migrate();

        new[]
        {
            new Role { Name = "Administrators"},
            new Role { Name = "OrganizationAdministrators"},
            new Role { Name = "OrganizationUsers"},
            new Role { Name = "Members"},
        }
        .ToList()
        .ForEach(p =>
        {
            if (!roleManager.RoleExistsAsync(p.Name!).Result)
                roleManager.CreateAsync(p).Wait();
        });

        return app;
    }

    public static IServiceCollection AddEkspertizaApi(this IServiceCollection services)
    {

        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
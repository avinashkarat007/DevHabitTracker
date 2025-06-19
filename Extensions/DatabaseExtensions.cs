using DevHabitTracker.Database;
using DevHabitTracker.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DevHabitTracker.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task ApplyMigrationsAsync(this WebApplication webApplication)
        {
            using IServiceScope scope = webApplication.Services.CreateScope();
            await using ApplicationDbContext applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await using ApplicationIdentityDbContext applicationIdentityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();
            try
            {
                await applicationDbContext.Database.MigrateAsync();
                webApplication.Logger.LogInformation("Application database migrations applied successfully!");

                await applicationIdentityDbContext.Database.MigrateAsync();
                webApplication.Logger.LogInformation("Identity database migrations applied successfully!");
            }
            catch (Exception ex)
            {
                webApplication.Logger.LogInformation(ex, "An error occured during applying database migrations!");
                throw;
            }
        }

        /// <summary>
        /// Creating user roles in the system.
        /// </summary>
        /// <param name="webApplication"></param>
        /// <returns></returns>
        public static async Task SeedInitialDataAsync(this WebApplication webApplication)
        {
            using IServiceScope scope = webApplication.Services.CreateScope();
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider
                            .GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
                if (!await roleManager.RoleExistsAsync(Roles.Member))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Member));
                }

                if (!await roleManager.RoleExistsAsync(Roles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

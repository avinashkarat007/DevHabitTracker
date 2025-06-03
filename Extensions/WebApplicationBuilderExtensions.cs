using DevHabitTracker.Database;
using Microsoft.AspNetCore.Identity;

namespace DevHabitTracker.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAuthenticationServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

            return builder;
        }
    }
}

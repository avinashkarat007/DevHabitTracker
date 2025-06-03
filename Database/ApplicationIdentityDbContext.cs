using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevHabitTracker.Database
{
    public sealed class ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options) : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("aspnet_users");
            builder.Entity<IdentityUser>().ToTable("aspnet_roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("aspnet_user_roles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("aspnet_role_claims");
            builder.Entity<IdentityUserClaim<string>>().ToTable("aspnet_user_claims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("aspnet_user_logins");
            builder.Entity<IdentityUserToken<string>>().ToTable("aspnet_user_tokens");
        }
    }
}

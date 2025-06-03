using DevHabitTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevHabitTracker.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(500);

            builder.Property(x => x.Email).HasMaxLength(100);

            builder.Property(x => x.IdentityId).HasMaxLength(500);

            builder.Property(x => x.Name).HasMaxLength(200);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasIndex(x => x.IdentityId).IsUnique();
        }
    }
}

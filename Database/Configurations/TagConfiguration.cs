using DevHabitTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevHabitTracker.Database.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(128);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(55);

            builder.Property(x => x.Description).HasMaxLength(55);

            builder.HasIndex(t => new { t.Name }).IsUnique();
        }
    }
}

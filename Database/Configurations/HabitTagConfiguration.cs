using DevHabitTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevHabitTracker.Database.Configurations
{
    public class HabitTagConfiguration : IEntityTypeConfiguration<HabitTag>
    {
        public void Configure(EntityTypeBuilder<HabitTag> builder)
        {
            builder.HasKey(ht => new { ht.HabitId, ht.TagId });
        }
    }

}

using DevHabitTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevHabitTracker.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
    {
        public DbSet<Habit> Habits { get; set; }
    }
}

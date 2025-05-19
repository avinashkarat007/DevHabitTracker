using DevHabitTracker.Entities;
using DevHabitTracker.Services.Interfaces;

namespace DevHabitTracker.Services
{
    public class HabitService : IHabitService
    {
        public List<Habit> GetHabits()
        {
            return new List<Habit>
                {
                    new Habit
                    {
                        Id = Guid.NewGuid(),
                        Name = "Write unit tests",
                        Description = "Spend 15 minutes writing or reviewing unit tests.",
                        Frequency = HabitFrequency.Daily,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-10),
                        LastCompletedAt = DateTime.UtcNow.AddDays(-1),
                        StreakCount = 5
                    },
                    new Habit
                    {
                        Id = Guid.NewGuid(),
                        Name = "Read tech blog",
                        Description = "Read a technical article or blog post.",
                        Frequency = HabitFrequency.Weekly,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow.AddDays(-30),
                        LastCompletedAt = DateTime.UtcNow.AddDays(-7),
                        StreakCount = 4
                    },
                    new Habit
                    {
                        Id = Guid.NewGuid(),
                        Name = "Contribute to GitHub",
                        Description = "Make at least one commit or PR to an open-source repo.",
                        Frequency = HabitFrequency.Monthly,
                        IsActive = false,
                        CreatedAt = DateTime.UtcNow.AddMonths(-2),
                        LastCompletedAt = null,
                        StreakCount = 0
                    }
                };
        }
    }
}

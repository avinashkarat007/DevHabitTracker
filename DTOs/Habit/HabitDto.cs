using DevHabitTracker.Entities;

namespace DevHabitTracker.DTOs.Habit
{
    public sealed class HabitDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public HabitFrequency Frequency { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastCompletedAt { get; set; }

        public int StreakCount { get; set; }
    }
}

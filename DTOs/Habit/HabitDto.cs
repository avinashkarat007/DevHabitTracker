using DevHabitTracker.Entities;

namespace DevHabitTracker.DTOs.Habit
{
    public sealed class HabitDto
    {
        public required string Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Frequency { get; set; }

        public string Priority { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastCompletedAt { get; set; }
    }
}

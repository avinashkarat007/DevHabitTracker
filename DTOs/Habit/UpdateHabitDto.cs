using DevHabitTracker.Entities;

namespace DevHabitTracker.DTOs.Habit
{
    public class UpdateHabitDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public HabitFrequency Frequency { get; set; }

        public bool IsActive { get; set; } = true;

        public HabitPriority Priority { get; set; }
    }
}

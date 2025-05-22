using DevHabitTracker.Entities;

namespace DevHabitTracker.DTOs.Habit
{
    public class CreateHabitDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public HabitFrequency Frequency { get; set; }
    }
}

namespace DevHabitTracker.Entities
{
    public class Habit
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for each habit

        public string Name { get; set; } = string.Empty; // e.g., "Write unit tests", "Read tech blog"

        public string? Description { get; set; } // Optional detailed explanation

        public HabitFrequency Frequency { get; set; } = HabitFrequency.Daily;

        public bool IsActive { get; set; } = true; // Whether the habit is currently tracked

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastCompletedAt { get; set; } // Last time the habit was done

        public int StreakCount { get; set; } = 0; // Number of consecutive successful completions
    }

    public enum HabitFrequency
    {
        Daily,
        Weekly,
        Monthly
    }

}

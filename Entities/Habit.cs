namespace DevHabitTracker.Entities
{
    public class Habit
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public string Name { get; set; } = string.Empty; // e.g., "Write unit tests", "Read tech blog"

        public string? Description { get; set; } // Optional detailed explanation

        public HabitFrequency Frequency { get; set; }

        public HabitPriority Priority { get; set; }

        public bool IsActive { get; set; } = true; // Whether the habit is currently tracked

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; } // Last time the habit was done

        public List<HabitTag>? HabitTags { get; set; } = new List<HabitTag>();

        public List<Tag>? Tags { get; set; }
    }

    public enum HabitFrequency
    {
        Daily,
        Weekly,
        Monthly
    }

    public enum HabitPriority
    {
        Low,
        Medium,
        High
    }
}

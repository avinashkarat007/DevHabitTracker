namespace DevHabitTracker.Entities
{
    public sealed class Tag
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public string Name { get; set; } = string.Empty; 

        public string? Description { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedAt { get; set; }

        public List<HabitTag>? HabitTags { get; set; } = new List<HabitTag>();
    }
}

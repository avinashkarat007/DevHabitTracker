namespace DevHabitTracker.Entities
{
    public class User
    {
        public string Id { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Name { get; set; } = default!;

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }

        public string IdentityId { get; set; } = default!;
    }
}

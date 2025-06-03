namespace DevHabitTracker.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Name { get; set; } = default!;

        public DateTime CreatedAtUtc { get; set; }

        public DateTime UpdatedAtUtc { get; set; }
    }
}

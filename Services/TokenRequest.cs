namespace DevHabitTracker.Services
{
    public sealed record TokenRequest(string UserId, string Email, IEnumerable<string> Roles);
}

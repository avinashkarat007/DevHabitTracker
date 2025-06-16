namespace DevHabitTracker.Services.Interfaces
{
    public interface IUserContext
    {
        Task<string?> GetUserIdAsync(CancellationToken cancellationToken = default);

    }
}

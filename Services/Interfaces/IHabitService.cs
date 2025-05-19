using DevHabitTracker.Entities;

namespace DevHabitTracker.Services.Interfaces
{
    public interface IHabitService
    {
        Task<List<Habit>> GetHabitsAsync();

        Task AddHabitsAsync(IEnumerable<Habit> habits);
    }
}

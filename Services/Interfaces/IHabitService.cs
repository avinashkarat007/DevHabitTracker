using DevHabitTracker.Entities;

namespace DevHabitTracker.Services.Interfaces
{
    public interface IHabitService
    {
        List<Habit> GetHabits();
    }
}

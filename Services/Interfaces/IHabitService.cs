using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.Entities;

namespace DevHabitTracker.Services.Interfaces
{
    public interface IHabitService
    {
        Task<List<HabitDto>> GetHabitsAsync();

        Task<HabitDto?> GetHabitByIdAsync(string id);

        Task CreateHabitsAsync(IEnumerable<CreateHabitDto> createHabitDtos);

        Task UpdateHabitAsync(string id, UpdateHabitDto updateHabitDto);

        Task<bool> IsHabitExists(string id);
    }
}

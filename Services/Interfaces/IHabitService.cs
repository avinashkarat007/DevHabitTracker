using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.Entities;

namespace DevHabitTracker.Services.Interfaces
{
    public interface IHabitService
    {
        Task<List<HabitDto>> GetHabitsAsync(HabitQueryParameters query);

        Task<HabitDto?> GetHabitByIdAsync(string id, string userId);

        Task CreateHabitsAsync(IEnumerable<CreateHabitDto> createHabitDtos, string userId);

        Task UpdateHabitAsync(string id, UpdateHabitDto updateHabitDto, string userId);

        Task<bool> IsHabitExists(string id);
    }
}

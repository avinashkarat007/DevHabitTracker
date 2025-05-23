using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.DTOs.Tag;

namespace DevHabitTracker.Services.Interfaces
{
    public interface ITagService  
    {
        Task<List<TagDto>> GetHabitsAsync();

        Task CreateTagAsync(IEnumerable<TagDto> createHabitDtos);
    }
}

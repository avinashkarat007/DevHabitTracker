using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.DTOs.Tag;

namespace DevHabitTracker.Services.Interfaces
{
    public interface ITagService  
    {
        Task<List<TagDto>> GetTagsAsync();

        Task CreateTagAsync(IEnumerable<TagDto> createTagDtos);

        Task<List<string>> GetExistingTagNamesAsync(List<string> tagNames);

    }
}

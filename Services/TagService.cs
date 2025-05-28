using DevHabitTracker.Database;
using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.DTOs.Tag;
using DevHabitTracker.Entities;
using DevHabitTracker.Extensions;
using DevHabitTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevHabitTracker.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateTagAsync(IEnumerable<TagDto> tagDto)
        {
            var entities = tagDto.Select(dto => dto.ToEntity());
            _context.Tags.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TagDto>> GetTagsAsync()
        {
            var tags = await _context.Tags.ToListAsync();
            return tags.Select(t => t.ToDto()).ToList();
        }

        public async Task<List<string>> GetExistingTagNamesAsync(List<string> tagNames)
        {
            // Normalize input: trim and make lower case for case-insensitive comparison
            var normalizedInputNames = tagNames
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Select(name => name.Trim().ToLower())
                .ToList();

            var existingNames = await _context.Tags
                .Where(tag => normalizedInputNames.Contains(tag.Name.ToLower()))
                .Select(tag => tag.Name)
                .ToListAsync();

            return existingNames;
        }

    }
}

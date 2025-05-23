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

        public async Task<List<TagDto>> GetHabitsAsync()
        {
            var tags = await _context.Tags.ToListAsync();
            return tags.Select(t => t.ToDto()).ToList();
        }
    }
}

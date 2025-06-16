using DevHabitTracker.Database;
using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.Entities;
using DevHabitTracker.Extensions;
using DevHabitTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DevHabitTracker.Services
{
    public class HabitService : IHabitService
    {
        private readonly ApplicationDbContext _context;

        public HabitService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HabitDto>> GetHabitsAsync(HabitQueryParameters query)
        {
            var habits = await _context.Habits.ToListAsync();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(query.search))
            {
                habits = habits
                    .Where(h =>
                        h.Name.Contains(query.search, StringComparison.OrdinalIgnoreCase) ||
                        (h.Description != null && h.Description.Contains(query.search, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            // Filter by frequency if valid
            if (query.frequency.HasValue)
            {
                var freqText = query.frequency.ToString();
                habits = habits
                    .Where(h => string.Equals(h.Frequency.ToString(), freqText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (query.priority.HasValue)
            {
                var prioText = query.priority.ToString();
                habits = habits
                    .Where(h => string.Equals(h.Priority.ToString(), prioText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }


            // Paging
            int skip = (query.Page - 1) * query.PageSize;
            habits = habits.Skip(skip).Take(query.PageSize).ToList();

            // Map to DTOs
            return habits.Select(h => h.ToDto()).ToList();
        }


        public async Task<HabitDto?> GetHabitByIdAsync(string id, string userId)
        {
            var habit = await _context.Habits
                .FirstOrDefaultAsync(h => h.UserId == userId && h.Id == id);
            return habit?.ToDto(); 
        } 

        public async Task CreateHabitsAsync(IEnumerable<CreateHabitDto> createHabitDto, string userId)
        {
            var entities = createHabitDto.Select(dto => dto.ToEntity(userId));
            _context.Habits.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHabitAsync(string id, UpdateHabitDto updateHabitDto, string userId)
        {
            // Get the habit entity from habit Dto
            var entity = updateHabitDto.ToEntity();

            // Get the habit record from db using the entity id.
            var habitEntitiyFromDb = await _context.Habits.FirstOrDefaultAsync(h => h.UserId == userId && h.Id == id);

            habitEntitiyFromDb.Name = entity.Name;
            habitEntitiyFromDb.Description = entity.Description;
            habitEntitiyFromDb.Frequency = entity.Frequency;
            habitEntitiyFromDb.IsActive = entity.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsHabitExists(string id)
        {
            Habit? habit = await _context.Habits.FirstOrDefaultAsync(h => h.Id == id);
            if (habit == null) return false;
            return true;
        }
    }
}

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

        public async Task<List<HabitDto>> GetHabitsAsync()
        {
            var habits = await _context.Habits.ToListAsync();
            return habits.Select(h => h.ToDto()).ToList();
        }

        public async Task<HabitDto?> GetHabitByIdAsync(string id)
        {
            var habit = await _context.Habits.FindAsync(id);
            return habit?.ToDto(); 
        }

        public async Task CreateHabitsAsync(IEnumerable<CreateHabitDto> updateHabitDto)
        {
            var entities = updateHabitDto.Select(dto => dto.ToEntity());
            _context.Habits.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHabitAsync(string id, UpdateHabitDto updateHabitDto)
        {
            // Get the habit entity from habit Dto
            var entity = updateHabitDto.ToEntity();

            // Get the habit record from db using the entity id.
            var habitEntitiyFromDb = await _context.Habits.FirstOrDefaultAsync(h => h.Id == id);

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

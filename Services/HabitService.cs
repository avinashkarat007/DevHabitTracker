using DevHabitTracker.Database;
using DevHabitTracker.Entities;
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

        public async Task<List<Habit>> GetHabitsAsync()
        {
            return await _context.Habits.ToListAsync();
        }

        public async Task AddHabitsAsync(IEnumerable<Habit> habits)
        {
            _context.Habits.AddRange(habits);
            await _context.SaveChangesAsync();
        }
    }
}

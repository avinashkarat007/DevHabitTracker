using DevHabitTracker.Entities;
using DevHabitTracker.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevHabitTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : ControllerBase
    {
        private readonly IHabitService _habitService;

        public HabitsController(IHabitService habitService)
        {
            _habitService = habitService;
        }

        [HttpGet("GetAllHabits")]
        public async Task<IActionResult> GetHabits()
        {
            var habits = await _habitService.GetHabitsAsync();
            return Ok(habits);
        }

        [HttpPost("AddHabits")]
        public async Task<IActionResult> AddHabits([FromBody] List<Habit> habits)
        {
            if (habits == null || !habits.Any())
                return BadRequest("Habit list is empty.");

            await _habitService.AddHabitsAsync(habits);
            return Ok("Habits added successfully.");
        }
    }
}

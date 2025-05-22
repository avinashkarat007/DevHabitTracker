using DevHabitTracker.DTOs.Habit;
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

        [HttpGet()]
        public async Task<ActionResult<List<HabitDto>>> GetHabits()
        {
            var habits = await _habitService.GetHabitsAsync();
            return Ok(habits);
        }

        // GET: api/habits/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HabitDto>> GetHabitById(string id)
        {
            var habit = await _habitService.GetHabitByIdAsync(id);
            if (habit == null)
            {
                return NotFound();
            }

            return Ok(habit);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateHabits([FromBody] List<CreateHabitDto> habits)
        {
            if (habits == null || !habits.Any())
            {
                return BadRequest("Habit list cannot be empty.");
            }

            await _habitService.CreateHabitsAsync(habits);
            return CreatedAtAction(nameof(GetHabits), null);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabit(string id, [FromBody] UpdateHabitDto habit)
        {
            //if (habits == null || !habits.Any())
            //{
            //    return BadRequest("Habit list cannot be empty.");
            //}

            await _habitService.UpdateHabitAsync(id, habit);
            return NoContent();
        }
    }
}

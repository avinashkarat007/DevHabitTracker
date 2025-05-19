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
        public ActionResult<List<Habit>> GetHabits()
        {
            var habits = _habitService.GetHabits();
            return Ok(habits);
        }
    }
}

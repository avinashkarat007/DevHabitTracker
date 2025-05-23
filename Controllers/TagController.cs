using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.DTOs.Tag;
using DevHabitTracker.Services;
using DevHabitTracker.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevHabitTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<HabitDto>>> GetHabits()
        {
            var tags = await _tagService.GetHabitsAsync();
            return Ok(tags);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateHabits([FromBody] List<TagDto> tags)
        {
            if (tags == null || !tags.Any())
            {
                return BadRequest("Habit list cannot be empty.");
            }

            await _tagService.CreateTagAsync(tags);
            return CreatedAtAction(nameof(GetHabits), null);
        }
    }
}

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
        public async Task<ActionResult<List<HabitDto>>> GetTags()
        {
            var tags = await _tagService.GetTagsAsync();
            return Ok(tags);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateTags([FromBody] List<TagDto> createTagDto)
        {
            if (createTagDto == null || !createTagDto.Any())
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Invalid Request",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Tag list cannot be empty.",
                    Instance = HttpContext.Request.Path
                });
            }

            // 1. Check for duplicate tag names in the input
            var duplicateNamesInInput = createTagDto
                .GroupBy(t => t.Name.Trim(), StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateNamesInInput.Any())
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Validation Failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Duplicate tag names found in request.",
                    Instance = HttpContext.Request.Path,
                    Extensions = { ["duplicateTagNames"] = duplicateNamesInInput }
                });
            }

            // 2. Check if any of these tag names already exist in the database
            var inputNames = createTagDto.Select(t => t.Name.Trim()).ToList();
            var existingNames = await _tagService.GetExistingTagNamesAsync(inputNames); 

            if (existingNames.Any())
            {
                return Conflict(new ProblemDetails
                {
                    Title = "Conflict",
                    Status = StatusCodes.Status409Conflict,
                    Detail = "Some tag names already exist in the database.",
                    Instance = HttpContext.Request.Path,
                    Extensions = { ["existingTagNames"] = existingNames }
                });
            }

            await _tagService.CreateTagAsync(createTagDto);
            return CreatedAtAction(nameof(GetTags), null);
        }
    }
}

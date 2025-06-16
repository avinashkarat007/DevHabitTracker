using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.Entities;
using DevHabitTracker.Services;
using DevHabitTracker.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DevHabitTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HabitsController : ControllerBase
    {
        private readonly IHabitService _habitService;
        private readonly IUserContext _userContext;

        public HabitsController(IHabitService habitService, IUserContext userContext)
        {
            _habitService = habitService;
            this._userContext = userContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<HabitDto>>> GetHabits([FromQuery] HabitQueryParameters query)
        {
            var habits = await _habitService.GetHabitsAsync(query);
            return Ok(habits);
        }


        // GET: api/habits/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HabitDto>> GetHabitById(string id)
        {
            string? userId = await _userContext.GetUserIdAsync();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var habit = await _habitService.GetHabitByIdAsync(id, userId);
            if (habit is null)
            {
                return NotFound();
            }

            return Ok(habit);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateHabits([FromBody] List<CreateHabitDto> habitsDto
            ,IValidator<CreateHabitDto> validator)
        {
            string? userId = await _userContext.GetUserIdAsync();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            // Null check.
            if (habitsDto == null || !habitsDto.Any())
            {
                return BadRequest(ProblemDetailsFactory.CreateProblemDetails(HttpContext,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid Request",
                detail: "Habit list cannot be empty."));
            }

            var allFailures = new List<FluentValidation.Results.ValidationFailure>();

            // Fluent validation checks.
            foreach (var habit in habitsDto)
            {
                var validationResult = await validator.ValidateAsync(habit);
                if (!validationResult.IsValid)
                {
                    allFailures.AddRange(validationResult.Errors);
                }
            }

            if (allFailures.Any())
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Validation Failed",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "One or more habits are invalid.",
                    Extensions =
                {
                    ["errors"] = allFailures.Select(e => new
                    {
                        Property = e.PropertyName,
                        Error = e.ErrorMessage
                    })
                }
                });
            }


            await _habitService.CreateHabitsAsync(habitsDto, userId);
            return CreatedAtAction(nameof(GetHabits), null);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabit(string id, [FromBody] UpdateHabitDto habit)
        {
            string? userId = await _userContext.GetUserIdAsync();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            if (!await _habitService.IsHabitExists(id))
            {
                return NotFound();
            }

            await _habitService.UpdateHabitAsync(id, habit, userId);
            return NoContent();
        }
    }
}

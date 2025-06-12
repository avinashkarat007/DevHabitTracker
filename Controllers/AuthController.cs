using DevHabitTracker.Database;
using DevHabitTracker.DTOs.Auth;
using DevHabitTracker.DTOs.User;
using DevHabitTracker.Entities;
using DevHabitTracker.Services;
using DevHabitTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevHabitTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IUserService _userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var (succeeded, identityResult, userId, accessToeken) = await _userService.RegisterUserAsync(registerUserDto);

            if (!succeeded)
            {
                return Problem(
                    detail: "Unable to register user, please try again",
                    statusCode: StatusCodes.Status400BadRequest,
                    extensions: new Dictionary<string, object?>
                    {
                { "errors", identityResult.Errors.ToDictionary(e => e.Code, e => e.Description) }
                    }
                );
            }

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = userId },
                new { UserId = userId }
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessToken = await _userService.Login(loginUserDto);

            if (accessToken == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(accessToken);
        }
    }
}

using DevHabitTracker.DTOs.User;
using DevHabitTracker.Entities;
using DevHabitTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DevHabitTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Member)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserContext _userContext;

        public UsersController(IUserService userService, IUserContext userContext)
        {
            _userService = userService;
            this._userContext = userContext;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(string id)
        {
            string? userId = await _userContext.GetUserIdAsync();

            if (id != userId)
            {
                return Forbid();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var identityId = await _userContext.GetUserIdAsync();

            if (string.IsNullOrWhiteSpace(identityId))
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByIdentityIdAsync(identityId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}

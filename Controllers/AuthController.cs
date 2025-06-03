using DevHabitTracker.Database;
using DevHabitTracker.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevHabitTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(UserManager<IdentityUser> userManager,
        ApplicationIdentityDbContext applicationIdentityDbContext,
        ApplicationDbContext applicationDbContext) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            // Create identity user.


            // Create app user.

            return Ok();

        }
    }
}

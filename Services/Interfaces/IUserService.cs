using DevHabitTracker.DTOs.Auth;
using DevHabitTracker.DTOs.User;
using DevHabitTracker.Entities;
using Microsoft.AspNetCore.Identity;

namespace DevHabitTracker.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(string id);

        Task<(bool Succeeded, IdentityResult IdentityResult, string? UserId, AccessTokensDto? accessToeken)> RegisterUserAsync(RegisterUserDto registerUserDto);

        Task<AccessTokensDto?> Login(LoginUserDto loginUserDto);

        Task<UserDto?> GetUserByIdentityIdAsync(string identityId);
    }
}

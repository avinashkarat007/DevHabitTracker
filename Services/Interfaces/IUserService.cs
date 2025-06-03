using DevHabitTracker.DTOs.User;

namespace DevHabitTracker.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(string id);
    }
}

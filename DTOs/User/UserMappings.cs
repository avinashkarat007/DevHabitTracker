using DevHabitTracker.DTOs.Auth;
using DevHabitTracker.Entities;

namespace DevHabitTracker.DTOs.User
{
    public static class UserMappings
    {
        public static DevHabitTracker.Entities.User ToEntity(this RegisterUserDto registerUserDto)
        {
            return new Entities.User
            {
                Id = $"u_{Guid.CreateVersion7()}",
                Name = registerUserDto.Name ,
                Email = registerUserDto.Email ,
                CreatedAtUtc = DateTime.UtcNow
            };
        }
    }
}

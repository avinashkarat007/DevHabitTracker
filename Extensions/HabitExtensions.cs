using DevHabitTracker.DTOs.Habit;
using DevHabitTracker.DTOs.Tag;
using DevHabitTracker.Entities;

namespace DevHabitTracker.Extensions
{
    public static class HabitExtensions
    {
        public static HabitDto ToDto(this Habit habit)
        {
            return new HabitDto
            {
                Id = habit.Id,
                Name = habit.Name,
                Description = habit.Description,
                Frequency = habit.Frequency,
                IsActive = habit.IsActive,
                CreatedAt = habit.CreatedAt,
                LastCompletedAt = habit.LastUpdatedAt
            };
        }

        public static Habit ToEntity(this HabitDto dto)
        {
            return new Habit
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Frequency = dto.Frequency,
                IsActive = dto.IsActive,
                CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt,
                LastUpdatedAt = dto.LastCompletedAt
            };
        }

        public static Habit ToEntity(this CreateHabitDto dto)
        {
            return new Habit
            {
                Id = $"h_{Guid.CreateVersion7()}",
                Name = dto.Name,
                Description = dto.Description,
                Frequency = dto.Frequency,
                IsActive = true,
                CreatedAt =  DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };
        }

        public static Habit ToEntity(this UpdateHabitDto dto)
        {
            return new Habit
            {
                Name = dto.Name,
                Description = dto.Description,
                Frequency = dto.Frequency,
                IsActive = dto.IsActive,
                LastUpdatedAt = DateTime.UtcNow
            };
        }

        public static TagDto ToDto(this Tag tag)
        {
            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description,
                CreatedAt = tag.CreatedAt
            };
        }


        public static Tag ToEntity(this TagDto dto)
        {
            return new Tag
            {
                Id = $"h_{Guid.CreateVersion7()}",
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };
        }
    }

}

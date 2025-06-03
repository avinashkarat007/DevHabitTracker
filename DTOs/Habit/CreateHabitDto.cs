using DevHabitTracker.Entities;
using FluentValidation;

namespace DevHabitTracker.DTOs.Habit
{
    public class CreateHabitDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public HabitFrequency Frequency { get; set; }

        public HabitPriority Priority { get; set; }
    }

    public sealed class CreateHabitDtoValidator : AbstractValidator<CreateHabitDto>
    {
        public CreateHabitDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(4);

            RuleFor(x => x.Description).MaximumLength(100);
        }

    }
}

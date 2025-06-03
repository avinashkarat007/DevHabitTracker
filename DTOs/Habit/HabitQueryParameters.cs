using DevHabitTracker.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevHabitTracker.DTOs.Habit
{
    public class HabitQueryParameters
    {
        [FromQuery]
        public string? search { get; set; }

        public HabitFrequency? frequency { get; set; }

        public HabitPriority? priority { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 5;
    }
}

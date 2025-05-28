namespace DevHabitTracker.Entities
{
    public class HabitTag
    {
        public string HabitId { get; set; }

        public Habit Habit { get; set; }

        public string TagId { get; set; }

        public Tag Tag { get; set; }

        public DateTime CreatedATUtc { get; set; }
    }
}

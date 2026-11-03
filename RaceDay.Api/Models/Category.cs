namespace RaceDay.Api.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        // Link back to Event
        public int EventId { get; set; }
        public Event Event { get; set; }

        // Add these properties (they were missing!)
        public string Name { get; set; }
        public string Description { get; set; }

        // Relationship: one Category -> many Enrolments
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}


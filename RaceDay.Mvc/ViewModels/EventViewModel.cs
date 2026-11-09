namespace RaceDay.Mvc.ViewModels
{
    public class EventViewModel
    {
        public int EventId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public double DistanceKm { get; set; }

        public string EventType { get; set; } = string.Empty;
    }
}
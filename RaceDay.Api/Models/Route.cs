namespace RaceDay.Api.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public double DistanceKm { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
    }
}

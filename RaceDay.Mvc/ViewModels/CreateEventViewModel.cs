using System.ComponentModel.DataAnnotations;

namespace RaceDay.Mvc.ViewModels
{
    public class CreateEventViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public double DistanceKm { get; set; }

        [Required]
        public string EventType { get; set; } = string.Empty;
    }
}

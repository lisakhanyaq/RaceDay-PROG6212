using System.ComponentModel.DataAnnotations;

namespace RaceDay.Api.Models
{
    public class Enrolment
    {
        [Key]
        public int EnrolmentId { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Add this property
        public string Status { get; set; } = "Pending";

        public Result Result { get; set; }
    }
}

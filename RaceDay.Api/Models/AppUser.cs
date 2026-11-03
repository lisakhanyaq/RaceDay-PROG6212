using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.Api.Models
{
    public class AppUser
    {
        [Key]   // Primary Key
        public int UserId { get; set; }

        // User details
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Role relationship
        public int RoleId { get; set; }
        public Role Role { get; set; }

        // Navigation: one User -> many Enrolments
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    }
}

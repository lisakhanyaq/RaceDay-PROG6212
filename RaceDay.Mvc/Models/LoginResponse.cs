namespace RaceDay.Mvc.Models
{
    public class LoginResponse
    {
        public string Message { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
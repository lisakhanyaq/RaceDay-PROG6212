namespace RaceDay.Api.Models
{
    public class Result
    {
        public int ResultId { get; set; }

        // Link to Enrolment
        public int EnrolmentId { get; set; }
        public Enrolment Enrolment { get; set; }

        // Race outcome fields
        public TimeSpan FinishTime { get; set; }
        public int Position { get; set; }
    }
}

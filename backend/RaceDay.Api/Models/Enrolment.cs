namespace RaceDay.Api.Models
{
    public class Enrolment
    {
        public int EnrolmentID { get; set; }
        public int ParticipantID { get; set; }
        public int CategoryID { get; set; }
        public DateTime EnrolmentDate { get; set; } = DateTime.Now;
        public string PaymentStatus { get; set; } = "Pending";

        public User? Participant { get; set; }
        public EventCategory? Category { get; set; }
    }
}
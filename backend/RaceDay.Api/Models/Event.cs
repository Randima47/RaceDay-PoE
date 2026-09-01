namespace RaceDay.Api.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public int OrganiserID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;

        public User? Organiser { get; set; }
        public List<EventCategory> Categories { get; set; } = new();
    }
}
namespace RaceDay.Api.DTOs
{
    // DTO for creating and updating an event
    public class EventDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public decimal DistanceKm { get; set; }
        public string EventType { get; set; } = string.Empty; // "Run", "Walk", or "Cycle"
    }
}

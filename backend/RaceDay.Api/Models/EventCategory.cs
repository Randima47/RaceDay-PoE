namespace RaceDay.Api.Models
{
    public class EventCategory
    {
        public int CategoryID { get; set; }
        public int EventID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal DistanceKM { get; set; }
        public decimal EntryFee { get; set; }

        public Event? Event { get; set; }
    }
}
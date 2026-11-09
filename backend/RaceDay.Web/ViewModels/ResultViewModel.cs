namespace RaceDay.Web.ViewModels
{
    public class ResultViewModel 
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string ParticipantName { get; set; } = string.Empty;
        public string FinishTime { get; set; } = string.Empty; // e.g., "00:45:12"
        public int TotalFinishers { get; set; }
         public int OverallPosition { get; set; }
        public int CategoryPosition { get; set; }
        }
    }


namespace RaceDay.Web.ViewModels
{
    public class CreateEventViewModel 
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = DateTime.Now.AddDays(7);
        public string Location { get; set; } = string.Empty;
        public string? BannerUrl { get; set; }
    }

    public class CaptureResultViewModel
    {
        public int EnrolmentID { get; set; }
        public string FinishTime { get; set; } = string.Empty;
        public int OverallPosition { get; set; }
        public int CategoryPosition { get; set; }
    }
}

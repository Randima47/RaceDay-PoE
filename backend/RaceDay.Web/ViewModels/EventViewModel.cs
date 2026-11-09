namespace RaceDay.Web.ViewModels
{
    public class EventViewModel 
    {
        public int EventID { get; set; }
        public int Id => EventID; 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string? BannerUrl { get; set; }
        public string OrganiserName { get; set; } = string.Empty;
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}

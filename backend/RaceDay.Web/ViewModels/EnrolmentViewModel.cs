namespace RaceDay.Web.ViewModels
{ 
    public class EnrolmentPageViewModel
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public int SelectedCategoryId { get; set; }
    }

    public class EnrolmentCreateViewModel
    {
        public int CategoryID { get; set; }
    }

    public class EventEnrolmentViewModel
    {
        public int EnrolmentID { get; set; }
        public string ParticipantName { get; set; } = string.Empty;
        public string ParticipantEmail { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public DateTime EnrolmentDate { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}

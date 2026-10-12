namespace RaceDay.Api.DTOs
{
    public class ResultDto
    {
        public int EnrolmentID { get; set; }
        public TimeSpan FinishTime { get; set; }
        public int? OverallPosition { get; set; }
        public int? CategoryPosition { get; set; }
    }
}
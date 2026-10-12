namespace RaceDay.Api.DTOs
{
    public class CategoryDto
    {
        public int EventID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal DistanceKM { get; set; }
        public decimal EntryFee { get; set; }
    }
}

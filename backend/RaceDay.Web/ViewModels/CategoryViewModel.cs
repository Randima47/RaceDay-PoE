namespace RaceDay.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty; // e.g., "Under 21" or "10km"
        public double MinAge { get; set; }
        public double MaxAge { get; set; }
          public string CategoryName { get; set; } = string.Empty;
            public double DistanceKM { get; set; }
            public decimal EntryFee { get; set; }
        }
    }


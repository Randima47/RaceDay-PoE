using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Models;

namespace RaceDay.Api.Data
{
    // Application database context managing entity connections and table mappings
    public class ApplicationDbContext : DbContext
    {
        // Initializes context with configured database options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Database tables
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventCategory> EventCategories { get; set; } = null!;
        public DbSet<Enrolment> Enrolments { get; set; } = null!;
        public DbSet<Result> Results { get; set; } = null!;

        // Configures table column precision and relationships using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Sets decimal precision for event distance 
            modelBuilder.Entity<EventCategory>()
                .Property(c => c.DistanceKM)
                .HasColumnType("decimal(5, 2)");

            // Sets decimal precision for entry fees 
            modelBuilder.Entity<EventCategory>()
                .Property(c => c.EntryFee)
                .HasColumnType("decimal(10, 2)");
        }
    }
}
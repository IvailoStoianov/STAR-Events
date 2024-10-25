using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Configurations;
using STAREvents.Data.Models;

namespace STAREvents.Web.Data
{
    public class STAREventsDbContext : IdentityDbContext
    {
        public STAREventsDbContext(DbContextOptions<STAREventsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Applying Configurations
            modelBuilder.ApplyConfiguration(new EventCategoryEntityConfiguration());

            //Seeding the data
            SeedDataConfiguration.ApplySeedData(modelBuilder);


        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
    }
}

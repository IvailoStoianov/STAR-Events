using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Configurations;
using STAREvents.Data.Models;

namespace STAREvents.Web.Data
{
    public class STAREventsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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
            modelBuilder.ApplyConfiguration(new UserEventAttendanceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityConfiguration());
            //Seeding the data
            SeedDataConfiguration.ApplySeedData(modelBuilder);


        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserEventAttendance> UsersEventAttendances { get; set; }
        public DbSet<EventCategory> EventsCategories { get; set; }
    }
}

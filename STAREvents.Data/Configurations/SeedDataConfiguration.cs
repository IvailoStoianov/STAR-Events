using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Extensions;
using STAREvents.Data.Models;

using static STAREvents.Common.FilePathConstants.SeedDataPaths;

namespace STAREvents.Data.Configurations
{
    public static class SeedDataConfiguration
    {
        public static void ApplySeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedDataFromJson<Category>(CategoriesSeedPath);
            modelBuilder.SeedDataFromJson<Venue>(VenuesSeedPath);
            modelBuilder.SeedDataFromJson<Organizer>(OrganizersSeedPath);
        }
    }
}

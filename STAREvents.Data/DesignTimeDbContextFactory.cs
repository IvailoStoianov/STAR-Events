using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using STAREvents.Web.Data;
namespace STAREvents.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<STAREventsDbContext>
    {
        public STAREventsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<STAREventsDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new STAREventsDbContext(optionsBuilder.Options);
        }
    }
}

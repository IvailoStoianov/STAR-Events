using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using static STAREvents.Common.ErrorMessagesConstants.DataMessages;

namespace STAREvents.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDataFromJson<TEntity>(this ModelBuilder modelBuilder, string filePath) where TEntity : class
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(baseDirectory, filePath);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException(string.Format(SeedFileNotFound, fullPath));
            }

            var jsonData = File.ReadAllText(fullPath);
            var data = JsonConvert.DeserializeObject<List<TEntity>>(jsonData);
            if (data != null)
            {
                modelBuilder.Entity<TEntity>().HasData(data);
            }
        }
    }
}

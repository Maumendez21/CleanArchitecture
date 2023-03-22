
using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;


namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers!.Any())
            {
                context.Streamers!.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("Insertando nuevos records a la db {context}", typeof(StreamerDbContext).Name);
            }
        }


        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer
                {
                    CreatedBy = "maumendez", 
                    Name = "HBO MAX", 
                    Url = "http://www.hbomax.com"
                },
                new Streamer
                {
                    CreatedBy = "maumendez", 
                    Name = "Paramount Plus", 
                    Url = "http://www.paramount.com"
                },
            };
        }
    }
}

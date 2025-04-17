using Microsoft.EntityFrameworkCore;
using VideoGameApiVsa.Entities;

namespace VideoGameApiVsa.Data
{
    public class VideoGameDbContext : DbContext
    {
        public VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : base(options)
        {
        }

        public DbSet<VideoGame> VideoGames { get; set; } 
    }
}

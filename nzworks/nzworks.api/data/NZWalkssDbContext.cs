using Microsoft.EntityFrameworkCore;
using nzworks.api.Models.Domain;

namespace nzworks.api.data
{
    public class NZWalkssDbContext : DbContext
    {
        public NZWalkssDbContext(DbContextOptions<NZWalkssDbContext> options) : base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    }
}

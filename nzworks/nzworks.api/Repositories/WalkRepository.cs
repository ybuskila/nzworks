using Microsoft.EntityFrameworkCore;
using nzworks.api.data;
using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public class WalkRepository : IWalksRepository
    {
        private readonly NZWalkssDbContext nZWalkssDbContext;

        public WalkRepository(NZWalkssDbContext nZWalkssDbContext)
        {
            this.nZWalkssDbContext = nZWalkssDbContext;
        }
        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = new Guid();
            await this.nZWalkssDbContext.AddAsync(walk);
            await this.nZWalkssDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await this.nZWalkssDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            nZWalkssDbContext.Walks.Remove(walk);
            await nZWalkssDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            var walks = this.nZWalkssDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)                
                .ToListAsync();
            return await walks;
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await this.nZWalkssDbContext.Walks
                .Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var walk1 = await this.nZWalkssDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(walk1 == null)
            {
                return null;
            }
            walk1.FullName = walk.FullName;
            walk1.WalkDifficultyId = walk.WalkDifficultyId;
            walk1.Region = walk.Region;
            walk1.Length = walk.Length;
            await this.nZWalkssDbContext.SaveChangesAsync();
            return walk1;
        }
    }
}

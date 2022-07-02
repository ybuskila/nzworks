using Microsoft.EntityFrameworkCore;
using nzworks.api.data;
using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public class WalkDifficlultyRepository : IWalkDificultyRepository
    {
        private readonly NZWalkssDbContext nZWalkssDbContext;

        public WalkDifficlultyRepository(NZWalkssDbContext nZWalkssDbContext)
        {
            this.nZWalkssDbContext = nZWalkssDbContext;
        }

        public async Task<WalkDifficulty> AddSync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = new Guid();
            await this.nZWalkssDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await this.nZWalkssDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var wd = await this.nZWalkssDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if(wd == null)
            {
                return null;
            }

            this.nZWalkssDbContext.WalkDifficulty.Remove(wd);
            await this.nZWalkssDbContext.SaveChangesAsync();
            return wd;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await this.nZWalkssDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            var wd = await this.nZWalkssDbContext.WalkDifficulty.FirstOrDefaultAsync(x=> x.Id == id);
            return wd;
        }

        public async Task<WalkDifficulty> UpdateSync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existsWD = this.nZWalkssDbContext.WalkDifficulty.FirstOrDefault(x => x.Id == id);
            if(existsWD == null)
            {
                return null;
            }

            existsWD.Code = walkDifficulty.Code;
            await nZWalkssDbContext.SaveChangesAsync();
            return existsWD;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using nzworks.api.data;
using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalkssDbContext nZWalkssDbContext;

        public RegionRepository(NZWalkssDbContext nZWalkssDbContext)
        {
            this.nZWalkssDbContext = nZWalkssDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await this.nZWalkssDbContext.Regions.ToListAsync();
        }
    }
}

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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = new Guid();
            await this.nZWalkssDbContext.Regions.AddAsync(region);
            await nZWalkssDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await this.nZWalkssDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            nZWalkssDbContext.Regions.Remove(region);
            await nZWalkssDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await this.nZWalkssDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await this.nZWalkssDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var region1 = nZWalkssDbContext.Regions.FirstOrDefault(x => x.Id == id);
            if(region1 == null)
            {
                return null;
            }

            region1.Code = region.Code;
            region1.Name = region.Name; 
            region1.Area = region.Area;
            region1.Lat = region.Lat;
            region1.Long = region.Long;
            region1.Population = region.Population;

            await nZWalkssDbContext.SaveChangesAsync();

            return region1;
        }
    }
}

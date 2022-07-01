using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public interface IRegionRepository
    {
        public Task<IEnumerable<Region>> GetAllAsync();
    }
}

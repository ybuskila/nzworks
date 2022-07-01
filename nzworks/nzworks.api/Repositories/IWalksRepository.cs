using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public interface IWalksRepository
    {
        public Task<IEnumerable<Models.Domain.Walk>> GetAllAsync();
        public Task<Walk> GetAsync(Guid id);
        public Task<Walk> AddAsync(Walk walk);        
        public Task<Walk> DeleteAsync(Guid id);
        public Task<Walk> UpdateAsync(Guid id, Walk walk);
    }
}

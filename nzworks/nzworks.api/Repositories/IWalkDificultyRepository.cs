using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public interface IWalkDificultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetAsync(Guid id);

        Task<WalkDifficulty> AddSync(WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> UpdateSync(Guid id, WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> DeleteAsync(Guid id);

    }
}

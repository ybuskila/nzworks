using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);

    }
}

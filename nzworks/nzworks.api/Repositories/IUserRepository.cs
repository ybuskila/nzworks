using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string passsword);
    }
}

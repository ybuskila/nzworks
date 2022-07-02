using Microsoft.EntityFrameworkCore;
using nzworks.api.data;
using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalkssDbContext nZWalkssDbContext;

        public UserRepository(NZWalkssDbContext nZWalkssDbContext)
        {
            this.nZWalkssDbContext = nZWalkssDbContext;
        }
        public async Task<User> AuthenticateAsync(string username, string passsword)
        {
            var user = await nZWalkssDbContext.Users.FirstOrDefaultAsync(x => x.UsreName.ToLower() == username && x.Password.ToLower() == passsword);
            if (user == null)
            {
                return null;
            }

            var userRoles = await nZWalkssDbContext.Users_Roles.Where(x => x.UserId == user.Id).ToListAsync();
            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await nZWalkssDbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}

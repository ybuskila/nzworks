using nzworks.api.Models.Domain;

namespace nzworks.api.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        List<User> Users = new List<User>()
        {
            //new User()
            //{
            //    FirstName="Read Only",
            //    LastName ="User",
            //    EmailAddress = "readonly@user.com",
            //    Id = Guid.NewGuid(),
            //    UsreName = "readonly@user.com",
            //    Password = "Readonly@user"
            //    //Roles = new List<string>(){"reader"}
            //},
            //new User()
            //{
            //    FirstName="Read Write",
            //    LastName ="User",
            //    EmailAddress = "readwrite@user.com",
            //    Id = Guid.NewGuid(),
            //    UsreName = "readwrite@user.com",
            //    Password = "readwrite@user"
            //    //Roles = new List<string>(){"reader", "writer"}
            //}
        };
        public async Task<User> AuthenticateAsync(string username, string passsword)
        {
            var user = Users.Find(x => x.UsreName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == passsword);

            return user;
        }
    }
}

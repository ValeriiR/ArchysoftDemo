
using D1.Data.Entities;
using D1.Data.Repositories.Abstract;


namespace D1.Data.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        public User GetUser(string login, string password)
        {
            return new User();
        }
    }
}

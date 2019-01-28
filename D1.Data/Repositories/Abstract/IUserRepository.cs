
using D1.Data.Entities;
using D1.Data.Repositories.Concrete;

namespace D1.Data.Repositories.Abstract
{
    public interface IUserRepository :  IRepository<User>
    {
        User GetUser(string login, string password);
    }
}

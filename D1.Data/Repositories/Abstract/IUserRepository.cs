
using D1.Data.Entities;

namespace D1.Data.Repositories.Abstract
{
   public interface IUserRepository
    {
        User GetUser(string login, string password);
    }
}

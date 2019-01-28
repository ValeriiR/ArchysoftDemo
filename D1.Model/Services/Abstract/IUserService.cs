using System.Collections.Generic;
using D1.Model.Users;


namespace D1.Model.Services.Abstract
{
    public interface IUserService
    {
       List<UserGridModel> Get();
    }
}

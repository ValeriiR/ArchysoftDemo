
using AutoMapper;
using D1.Data.Entities;
using D1.Model.Users;

namespace D1.Model.Mappings
{
   public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserGridModel>();
        }
    }
}

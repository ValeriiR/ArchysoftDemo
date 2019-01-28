using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using D1.Data.Repositories.Abstract;
using D1.Model.Services.Abstract;
using D1.Model.Users;

namespace D1.Model.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public List<UserGridModel> Get()
        {          
            return _userRepository.Get().Select(x => _mapper.Map<UserGridModel>(x)).ToList();
        }
    }
}

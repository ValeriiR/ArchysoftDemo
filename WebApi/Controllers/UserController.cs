using System.Collections.Generic;
using D1.Model.Services.Abstract;
using D1.Model.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WebApi.Controllers
{
    public class UserController:Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

     
        [HttpGet]
        [Route(Routes.Users)]
        public ApiResponse<List<UserGridModel>> GetUsers()
        {
            List<UserGridModel> users = _userService.Get();
            return new ApiResponse<List<UserGridModel>>(users);
        }
    }
}

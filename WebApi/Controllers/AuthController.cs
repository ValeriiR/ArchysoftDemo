using D1.Model;
using D1.Model.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;


namespace WebApi.Controllers
{
    public class AuthController:Controller
    {
        private readonly IAuthService _authService;
    

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
      
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(Routes.Login)]
        public ApiResponse<TokenModel> Login([FromBody]LoginModel loginModel)
        {
            
            TokenModel token = this._authService.Login(loginModel);
            return new ApiResponse<TokenModel>(token);
            
        }
    }
}
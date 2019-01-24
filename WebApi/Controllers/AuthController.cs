using D1.Model;
using D1.Model.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApi.Model;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WebApi.Controllers
{
    public class AuthController
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
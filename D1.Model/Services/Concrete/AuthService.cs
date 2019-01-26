
using D1.Data.Entities;
using D1.Data.Repositories.Abstract;
using D1.Model.Services.Abstract;
using D1.Model.Exceptions;

namespace D1.Model.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repositoryService;

        public AuthService(IUserRepository repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public TokenModel Login(LoginModel loginModel)
        {
            User user = _repositoryService.GetUser(loginModel.Login, loginModel.Password);

            if (user == null)
            {
                throw new BusinessException("Invalid login or password", -2);
            }
            else
            {
                 return GenerateToken(user);             
            }

        }

        private TokenModel GenerateToken(User user)
        {
            return new TokenModel();
        }
    }
}

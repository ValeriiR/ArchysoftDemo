

namespace D1.Model.Services.Abstract
{
    public interface IAuthService
    {
        TokenModel Login(LoginModel loginModel);
    }
}

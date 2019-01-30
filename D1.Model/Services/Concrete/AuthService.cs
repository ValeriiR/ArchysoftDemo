
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using D1.Data.Entities;
using D1.Data.Repositories.Abstract;
using D1.Model.Authentification;
using D1.Model.Services.Abstract;
using D1.Model.Exceptions;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace D1.Model.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISettingsService _settingsService;
        private readonly IEmailService _emailService;

        public AuthService(IUserRepository repositoryService, ISettingsService settingsService, IEmailService emailService)
        {
            _userRepository = repositoryService;
            _settingsService = settingsService;
            _emailService = emailService;
        }

        public TokenModel Login(LoginModel loginModel)
        {
            User user = _userRepository.GetUser(loginModel.Login, loginModel.Password);

            if (user == null)
            {
                throw new BusinessException("Invalid login or password", -2);
            }
            else
            {
                return GenerateToken(user);
            }

        }


        public void ForgotPassword(ForgotPasswordModel email)
        {
            User user = _userRepository.GetUser(email.Email);

            if (user == null)
            {
                throw new BusinessException("Not find user with current email", -2);
            }
      
            var token =  _userRepository.GeneratePasswordResetToken(user);

        
            string url = $"https://localhost:44343/auth/recover-password/?id={user.Id}&token={token}";

            _emailService.SendEmailAsync(user.Email, "Recover Password", $"Для сброса пароля пройдите по ссылке: {url}");
        }


        public void RecoverPassword(RecoverPasswordModel model)
        {
            User user = _userRepository.GetUserById(Guid.Parse(model.UserId));
            if (user == null)
            {
                throw new BusinessException("User not found", -2);
            }

            _userRepository.UpdatePassword(user, model.Password,model.Token);
        }

       

        private TokenModel GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.JwtSettings.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays);

            var jwtToken = new JwtSecurityToken(
                _settingsService.JwtSettings.Issuer,
                null,
                claims,
                expires: expires,
                signingCredentials: signingCredentials
                );

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresIn = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays)
            };
        }
    }
}

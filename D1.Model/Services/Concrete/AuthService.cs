
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using D1.Data.Entities;
using D1.Data.Repositories.Abstract;
using D1.Model.Services.Abstract;
using D1.Model.Exceptions;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace D1.Model.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repositoryService;
        private readonly ISettingsService _settingsService;

        public AuthService(IUserRepository repositoryService, ISettingsService settingsService)
        {
            _repositoryService = repositoryService;
            _settingsService = settingsService;
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

            return  new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresIn = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays)            
            };
        }
    }
}

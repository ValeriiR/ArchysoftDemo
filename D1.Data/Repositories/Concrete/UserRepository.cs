using System;
using System.Reflection.Metadata.Ecma335;
using D1.Data.Entities;
using D1.Data.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace D1.Data.Repositories.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
       
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(DataContext dataContext, UserManager<User> userManager, IPasswordHasher<User> passwordHasher) : base(dataContext)
        {
          
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public User GetUser(string email, string password)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) ==
                    PasswordVerificationResult.Success)
                {
                    return user;
                }
            }

            return null;
        }



        public User GetUser(string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                return user;
            }
          
            return null;     
        }

        public User GetUserById(Guid id)
        {
            var user = _userManager.FindByIdAsync(id.ToString()).Result;
          
            if (user != null)
            {
                return user;
            }

            return null;
        }


        public string GeneratePasswordResetToken(User user)
        {            
           return _userManager.GeneratePasswordResetTokenAsync(user).Result;
        }

        public bool VerifyUserToken(User user,string token)
        {
          
            return _userManager.VerifyUserTokenAsync(user, "password reset token provider", "reset password",token).Result;
        }

        public void UpdatePassword(User user, string password)
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
            SaveChanges();
        }
    }
}

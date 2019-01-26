﻿using System;
using System.Collections.Generic;
using System.Text;
using D1.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace D1.Data
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;

        public DatabaseInitializer(DataContext dataContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public void Initialize()
        {
            _dataContext.Database.Migrate();

            if (!_dataContext.Users.Any())
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@email.com",
                    EmailConfirmed = true
                };
                var result = _userManager.CreateAsync(user, "admin").Result;
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}

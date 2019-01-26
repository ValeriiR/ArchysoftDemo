﻿using System;
using System.Collections.Generic;
using System.Text;
using D1.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace D1.Data
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_DEVELOPMENT");

            var connection = environment == "Development" ? _configuration.GetConnectionString("DataContext") : Environment.GetEnvironmentVariable("D1_DATACONTEXT") ?? "";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
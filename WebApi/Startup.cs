using System;
using D1.Data.Repositories.Abstract;
using D1.Model.Services.Abstract;
using D1.Model.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using D1.Data.Repositories.Concrete;
using D1.Model;
using FluentValidation.AspNetCore;
using Serilog;
using Serilog.Configuration;
using WebApi.Utilites.Filters;
using WebApi.Utilites.Middleware;
using D1.Data;
using D1.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedEmail = true;

            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(Configuration)
                .CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            services.AddSingleton(Log.Logger);

            //LoggerConfiguration v=new LoggerConfiguration();
            //   LoggerSettingsConfiguration vv = v.ReadFrom;
            //   LoggerConfiguration vvv = vv.Configuration(Configuration);
            //   Log.Logger  = vvv.CreateLogger();


            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidationModelStateFilter());

            }).AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<LoginModel>());

            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<IUserRepository, UserRepository>();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDatabaseInitializer databaseInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                databaseInitializer.Initialize();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

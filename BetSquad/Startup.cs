using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BetSquad.Models;
using BetSquad.Services;
using BetSquad.Core.Domain;
using BetSquad.Infrastructure.Repository;
using BetSquad.Infrastructure.Services;
using BetSquad.Core.Repository;
using AutoMapper;
using BetSquad.Infrastructure.Mapper;
using BetSquad.Infrastructure.Initializer;

namespace BetSquad
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
               
            }
            );

            services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IRepository<ApplicationRole>, Repository<ApplicationRole>>();
            services.AddScoped<IRepository<ApplicationUserRole>, Repository<ApplicationUserRole>>();
            services.AddScoped<IRepository<ApplicationUser>, Repository<ApplicationUser>>();
            services.AddScoped<IRepository<Game>, Repository<Game>>();
            services.AddScoped<IRepository<Result>, Repository<Result>>();
            services.AddScoped<IRepository<Team>, Repository<Team>>();
            services.AddScoped<IRepository<Bet>, Repository<Bet>>();
            services.AddScoped<IRepository<FinishedBet>, Repository<FinishedBet>>();
            services.AddTransient<IInitializer, Initializer>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddMvc();

           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
       

    }
}

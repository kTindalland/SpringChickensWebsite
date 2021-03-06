using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpringChickens.Services;
using Interfaces;
using Interfaces.Services;
using Interfaces.Database;
using Database;
using SpringChickens.Models;
using SpringChickens.Factories;
using Interfaces.Factories;
using Microsoft.AspNetCore.Http;

namespace SpringChickens
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
            services.AddControllersWithViews();

            services.AddTransient<IUnitOfWork, DatabaseUnitOfWork>();

            services.AddSingleton<ICryptographyService, CryptographyService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IPasswordFilterPredicate, PasswordFilterPredicate>();
            services.AddTransient<IUsernameFilterPredicate, UsernameFilterPredicate>();
            services.AddSingleton<ICredentialHoldingService, CredentialHoldingService>();
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            services.AddTransient<IPasswordResetService, PasswordResetService>();
            services.AddSingleton<ICalendarHelpingService, CalendarHelpingService>();
            services.AddTransient<ISearchUsersService, SearchUsersService>();

            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(5));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}

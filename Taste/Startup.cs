using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using Taste.DataAccess;
using Taste.DataAccess.Data.Repository;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Utility;

namespace Taste
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews(options =>
            {
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
            .AddRazorRuntimeCompilation();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddAuthentication()
                        .AddFacebook(facebookOptions =>
                        {
                            facebookOptions.AppId = "287306226351622";
                            facebookOptions.AppSecret = "502449dbc27950a2966c5dd2a8304d18";
                        });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "68737654537-ojmd61fskreg5dulshi1du8osp01hupk";
                    options.ClientSecret = "w_gYNfZJ4ekoVvvhqTLMwRjm";
                });

            services.AddAuthentication()
                    .AddMicrosoftAccount(microsoftAccount =>
                    {
                        microsoftAccount.ClientId = "b335392e-46c1-4605-a79a-d7e293211845";
                        microsoftAccount.ClientSecret = "zdqutjF1vSW6Y_.taSxSEr4-N92S67u.Gm";
                    });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddSingleton(typeof(SignInManager<ApplicationUser>));
            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = true;
                options.LowercaseQueryStrings = true;
                options.LowercaseUrls = true;
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc();
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
        }
    }
}

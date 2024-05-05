using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ChatApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method configures services and middleware.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Configure cookie authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "YourAppNameCookie";
                    options.LoginPath = "/Home/Index"; // Redirect to login page if unauthorized
                    options.AccessDeniedPath = "/Home/AccessDenied"; // Redirect if access is denied
                });

            // Configure authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("StudentPolicy", policy => policy.RequireClaim("Role", "student"));
                options.AddPolicy("SupervisorPolicy", policy => policy.RequireClaim("Role", "supervisor"));
                options.AddPolicy("LecturerPolicy", policy => policy.RequireClaim("Role", "lecturer"));
            });
        }

        // This method configures the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Enable authentication middleware
            app.UseAuthentication();

            // Enable authorization middleware
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

#region IMPORTS
using Alfasoft.Interface;
using Alfasoft.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication2;
#endregion

namespace Alfasoft
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
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(Configuration.GetConnectionString("MariaDB"), new MySqlServerVersion(new Version(8, 0, 26))));

            //services.AddDbContext<ApplicationDbContext>(
            //   options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAvatarService, AvatarService>();
            services.AddScoped<ContactService>();
            services.AddScoped<CountriesService>();

            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtConfig:Issuer"],
                    ValidAudience = Configuration["JwtConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"]))
                };
            });
        }

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "contactStatistics",
                    pattern: "Contacts/ContactStatistics",
                    defaults: new { controller = "ContactStatistics", action = "ContactStatistics" });

                endpoints.MapControllerRoute(
                    name: "people",
                    pattern: "People/{action=Index}/{id?}",
                    defaults: new { controller = "People" });

                endpoints.MapControllerRoute(
                    name: "home",
                    pattern: "Home/{action=Index}",
                    defaults: new { controller = "Home" });
            });
        }
    }
}



#region IMPORTS
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using WebApplication2;
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));

var connectionString = builder.Configuration.GetConnectionString("MariaDB");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion)
);

builder.Services.AddControllersWithViews();
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<ApplicationDbContext>();

    if (dbContext != null && dbContext.Database.GetDbConnection().State != ConnectionState.Open)
    {
        dbContext.Database.OpenConnection();

        dbContext.Database.EnsureCreated();
    }

}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<ApplicationDbContext>();

    if (dbContext != null && dbContext.Database.GetDbConnection().State != ConnectionState.Open)
    {
        dbContext.Database.OpenConnection();

        dbContext.Database.EnsureCreated();
    }

}

void ConfigureServices(IServiceCollection services)
{
    var builder = WebApplication.CreateBuilder(args);

    var jwtConfig = builder.Configuration.GetSection("JwtConfig");
    var secretKey = jwtConfig["Secret"];
    var key = Encoding.ASCII.GetBytes(secretKey);

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"]
        };
    });

    services.AddControllersWithViews();
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

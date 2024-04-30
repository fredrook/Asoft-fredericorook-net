#region IMPORTS
using Alfasoft;
using Microsoft.EntityFrameworkCore;
using WebApplication2;
#endregion

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    context.Database.Migrate();
                }
                else
                {
                    Console.WriteLine("A aplicação está rodando em um ambiente diferente de desenvolvimento. As migrações não serão aplicadas automaticamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao aplicar migrações: " + ex.Message);
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        })
        .ConfigureServices((hostContext, services) =>
        {
            var configuration = hostContext.Configuration;
            services.AddSingleton(configuration);

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
            var connectionString = configuration.GetConnectionString("MariaDB");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

            //var configuration = hostContext.Configuration;
            //services.AddSingleton(configuration);
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        });
}


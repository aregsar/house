using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using house.Data;
using Microsoft.EntityFrameworkCore.Design;

namespace house
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .ConfigureAppConfiguration((hostingContext, configurationBuilder) => {
                    configurationBuilder.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                    configurationBuilder.AddJsonFile("appsettings.json"
                                                     , optional: false
                                                     , reloadOnChange: true)
                                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, loggingBuilder) => {
                    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                })
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            SeedDatabase(host);

            host.Run();
        }


        private static void SeedDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<HouseDbContext>();

                    context.Database.Migrate();

                    house.Data.Factories.HouseFactory.CreateHouses(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "error seeding database");
                }
            }

        }
    }

    //for EF migration tooling
    //https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
    //dotnet ef migrations add create_houses_table
    public class HouseContextFactory : IDesignTimeDbContextFactory<HouseDbContext>
    {
        public HouseDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HouseDbContext>();
            optionsBuilder.UseSqlite("Data Source=House.db");
            return new HouseDbContext(optionsBuilder.Options);
        }
    }
}

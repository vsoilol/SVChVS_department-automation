using System;
using System.Threading.Tasks;
using DepartmentAutomation.Infrastructure.Identity;
using DepartmentAutomation.Infrastructure.Persistence;
using DepartmentAutomation.Shared.Logger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DepartmentAutomation.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = ProjectLoggerConfiguration.GetLoggerConfiguration("DepartmentAutomation");
            Log.Information($"Starting host at {DateTime.Now}");

            var host = CreateHostBuilder(args).Build();

            await CreateDbIfNotExists(host);
            Log.Information(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty);
            Log.Information(Environment.GetEnvironmentVariable("DATABASE_URL") ?? string.Empty);

            await host.RunAsync();
        }

        private static async Task CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var context = services.GetRequiredService<DepartmentAutomationContext>();

                    /*if (context.Database.IsSqlServer())
                    {
                        await context.Database.MigrateAsync();
                    }*/

                    var userManager = services.GetRequiredService<ApplicationUserManager>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await DepartmentAutomationContextSeed.SeedDefaultUserAsync(userManager, roleManager);
                    await DepartmentAutomationContextSeed.SeedSampleDataAsync(context, userManager);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
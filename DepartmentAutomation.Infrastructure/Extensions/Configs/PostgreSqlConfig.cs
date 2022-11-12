using System;
using DepartmentAutomation.Infrastructure.Persistence;
using DepartmentAutomation.Shared.StringDecryptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DepartmentAutomation.Infrastructure.Extensions.Configs
{
    public static class PostgreSqlConfig
    {
        public static void SetupPostgreSql(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnectionString = string.Empty;
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isProduction = environment == Environments.Production;

            if (isProduction)
            {
                var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                var databaseUri = new Uri(connectionUrl);

                string db = databaseUri.LocalPath.TrimStart('/');
                string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
            
                sqlConnectionString = $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
            }
            else
            {
                sqlConnectionString = configuration.GetDecryptedConnectionString("DefaultConnectionPostgre");
            }

            services.AddDbContext<DepartmentAutomationContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly(typeof(DepartmentAutomationContext).Assembly.FullName)));
        }
    }
}
using DepartmentAutomation.Infrastructure.Persistence;
using DepartmentAutomation.Shared.StringDecryptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentAutomation.Infrastructure.Extensions.Configs
{
    public static class SqlServerConfig
    {
        public static void SetupSqlServer(this IServiceCollection services,
            IConfiguration configuration)
        {
            var sqlConnectionString = configuration.GetDecryptedConnectionString("DefaultConnectionSQLServer");

            services.AddDbContext<DepartmentAutomationContext>(options =>
            {
                options.UseSqlServer(
                    sqlConnectionString,
                    b => b.MigrationsAssembly(typeof(DepartmentAutomationContext).Assembly.FullName));
                options.EnableSensitiveDataLogging();
            });
        }
    }
}
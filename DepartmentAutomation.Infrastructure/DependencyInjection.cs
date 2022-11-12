using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities.UserInfo;
using DepartmentAutomation.Infrastructure.Extensions.Configs;
using DepartmentAutomation.Infrastructure.Identity;
using DepartmentAutomation.Infrastructure.Persistence;
using DepartmentAutomation.WordDocument;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DepartmentAutomation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isSqlServer = environment == "Development.SqlServer";

            if (isSqlServer)
            {
                services.SetupSqlServer(configuration);
            }
            else
            {
                services.SetupPostgreSql(configuration);
            }

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<DepartmentAutomationContext>());

            services.SetupIdentity();

            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.Replace(ServiceDescriptor
                .Scoped<IUserValidator<ApplicationUser>, ApplicationUserValidator<ApplicationUser>>());

            services.AddWordDocumentInfrastructure(configuration);

            return services;
        }
    }
}
using DepartmentAutomation.Domain.Entities.UserInfo;
using DepartmentAutomation.Infrastructure.Identity;
using DepartmentAutomation.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DepartmentAutomation.Infrastructure.Extensions.Configs
{
    public static class IdentityConfig
    {
        public static void SetupIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DepartmentAutomationContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddErrorDescriber<ApplicationIdentityErrorDescriber>();

            services.Configure<IdentityOptions>(options =>
            {
                var allowed = options.User.AllowedUserNameCharacters
                              + "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
                options.User.AllowedUserNameCharacters = allowed;
                options.User.RequireUniqueEmail = true;
            });
        }
    }
}
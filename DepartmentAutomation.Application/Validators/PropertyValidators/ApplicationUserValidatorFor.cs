using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities.UserInfo;
using DepartmentAutomation.Domain.Enums;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Validators.PropertyValidators
{
    public class ApplicationUserValidatorFor<T> : IAsyncPropertyValidator<T, string>
    {
        private readonly IApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserValidatorFor(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> IsValidAsync(ValidationContext<T> context, string value, CancellationToken cancellation)
        {
            var user = await _context.ApplicationUser.FirstOrDefaultAsync(_ => _.Id == value,
                cancellationToken: cancellation);

            if (user is null)
            {
                return false;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return userRoles.Contains(Role.Teacher.ToString());
        }

        public string GetDefaultMessageTemplate(string errorCode)
            => "User data must be a valid.";

        public string Name => "NotNullValidator";
    }
}
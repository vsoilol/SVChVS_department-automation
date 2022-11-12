using System.Linq;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.Common;
using DepartmentAutomation.Application.Contracts.Responses.Identity;
using DepartmentAutomation.Domain.Entities.UserInfo;
using DepartmentAutomation.Shared.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ITokenService _tokenService;

        public IdentityService(
            ApplicationUserManager userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        /*public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser is not null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exist" },
                };
            }

            var newUserId = Guid.NewGuid();
            var newUser = new ApplicationUser
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = email,
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(_ => _.Description),
                };
            }

            await _userManager.AddToRoleAsync(newUser, nameof(Role.SimpleUser));

            return await _tokenService.GenerateAuthenticationResultForUser(newUser);
        }*/

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" },
                };
            }
            
            if (!user.IsActive)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User is not active" },
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" },
                };
            }

            return await _tokenService.GenerateAuthenticationResultForUser(user);
        }

        public async Task<AuthenticationResult> LoginByFullNameAsync(string name, string surname, 
            string patronymic, string password)
        {
            var user = await _userManager.FindUserByFullName(name, surname, patronymic);

            if (user is null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" },
                };
            }
            
            if (!user.IsActive)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User is not active" },
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" },
                };
            }

            return await _tokenService.GenerateAuthenticationResultForUser(user);
        }

        public async Task<ChangePasswordResult> ChangePasswordAsync(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new ChangePasswordResult
                {
                    Errors = new[] { "User does not exist" },
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                return new ChangePasswordResult
                {
                    Errors = result.Errors.Select(_ => _.Description),
                };
            }

            return new ChangePasswordResult
            {
                Success = true,
                NewPassword = newPassword,
            };
        }

        public async Task<ChangePasswordResult> ChangePasswordAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                return new ChangePasswordResult
                {
                    Errors = new[] { "User does not exist" },
                };
            }

            var newPassword = SecurePassword.GenerateRandomPassword();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
            {
                return new ChangePasswordResult
                {
                    Errors = result.Errors.Select(_ => _.Description),
                };
            }

            await _tokenService.RevokeTokenAsync(user);

            return new ChangePasswordResult
            {
                Success = true,
                NewPassword = newPassword,
            };
        }

        public async Task<ResultInfo> ActivateUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                return new ResultInfo
                {
                    Errors = new[] { "User does not exist" },
                };
            }

            if (user.IsActive)
            {
                return new ResultInfo
                {
                    Errors = new[] { "User already activate" },
                };
            }

            user.IsActive = true;
            await _userManager.UpdateAsync(user);

            return new ResultInfo
            {
                Success = true,
            };
        }

        public async Task<ResultInfo> DeactivateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                return new ResultInfo
                {
                    Errors = new[] { "User does not exist" },
                };
            }

            if (!user.IsActive)
            {
                return new ResultInfo
                {
                    Errors = new[] { "User already deactivate" },
                };
            }

            user.IsActive = false;
            await _userManager.UpdateAsync(user);

            return new ResultInfo
            {
                Success = true,
            };
        }
    }
}
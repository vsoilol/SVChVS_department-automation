using DepartmentAutomation.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Contracts.Responses.Identity;
using DepartmentAutomation.Domain.Entities.UserInfo;

namespace DepartmentAutomation.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<AuthenticationResult> GenerateAuthenticationResultForUser(ApplicationUser user);

        Task<AuthenticationResult> RefreshTokenAsync(string refreshToken);

        Task<AuthenticationResult> RevokeTokenAsync(string refreshToken);

        Task RevokeTokenAsync(ApplicationUser user);
    }
}
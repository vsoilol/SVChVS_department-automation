using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Application.Common.Options;
using DepartmentAutomation.Application.Contracts.Responses.Identity;
using DepartmentAutomation.Domain.Entities.UserInfo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DepartmentAutomation.Infrastructure.Identity
{
    public class TokenService : ITokenService
    {
            private readonly IApplicationDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly JwtSettings _jwtSettings;

            public TokenService(
                IApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                JwtSettings jwtSettings)
            {
                _context = context;
                _userManager = userManager;
                _jwtSettings = jwtSettings;
            }

        public async Task<AuthenticationResult> GenerateAuthenticationResultForUser(ApplicationUser user)
        {
            var token = await GenerateJwtTokenAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();

            var refreshToken = await GenerateRefreshToken(token, user);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token,
            };
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken)
        {
            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(_ => _.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has expired" } };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token has been used" } };
            }
            
            var user = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
            
            if (!user.IsActive)
            {
                return new AuthenticationResult { Errors = new[] { "User is not active" } };
            }

            storedRefreshToken.Used = true;
            await _context.SaveChangesAsync();
            
            return await GenerateAuthenticationResultForUser(user);
        }

        public async Task<AuthenticationResult> RevokeTokenAsync(string refreshToken)
        {
            var storedRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(_ => _.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult { Errors = new[] { "This refresh token does not exist" } };
            }

            storedRefreshToken.Used = true;
            await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
            };
        }

        public async Task RevokeTokenAsync(ApplicationUser user)
        {
            await _context.RefreshTokens
                .Where(_ => _.UserId == user.Id && !_.Used)
                .ForEachAsync((token) => token.Used = true);

            await _context.SaveChangesAsync();
        }

        private async Task<SecurityToken> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("firstName", user.UserName),
                new Claim("surname", user.Surname),
                new Claim("patronymic", user.Patronymic),
                new Claim("departmentId", user.DepartmentId.ToString()),
                new Claim("id", user.Id),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(_ => new Claim(ClaimTypes.Role, _)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            return tokenHandler.CreateToken(tokenDescriptor);
        }

        private async Task<RefreshToken> GenerateRefreshToken(SecurityToken jwtToken, ApplicationUser user)
        {
            RefreshToken refreshToken;

            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                refreshToken = new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    JwtId = jwtToken.Id,
                    UserId = user.Id,
                    CreationDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddMonths(6),
                };
            }

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }
    }
}
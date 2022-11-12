using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Requests;
using DepartmentAutomation.Application.Contracts.Requests.Identity;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.Identity;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Shared.Logger;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DepartmentAutomation.Web.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(
            IIdentityService identityService,
            ITokenService tokenService,
            ILogger<IdentityController> logger)
        {
            _identityService = identityService;
            _tokenService = tokenService;
            _logger = logger;
        }

        /// <summary>
        /// Register user use email and password
        /// </summary>
        /// <response code="200">Return jwtToken and RefreshToken</response>
        /// <response code="400">Error when register user</response>
        /*[HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors,
                });
            }

            // Response.SetRefreshTokenCookie(authResponse.RefreshToken);
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
            });
        }*/
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors,
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
            });
        }

        [HttpPost(ApiRoutes.Identity.LoginByFullName)]
        public async Task<IActionResult> LoginByFullName([FromBody] UserLoginByFullNameRequest request)
        {
            var authResponse = await _identityService
                .LoginByFullNameAsync(request.Name, request.Surname, request.Patronymic, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors,
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
            });
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenInfo refreshToken)
        {
            var authResponse = await _tokenService.RefreshTokenAsync(refreshToken.RefreshToken);

            if (!authResponse.Success)
            {
                _logger.LogInformationWithProjectTemplate(
                    nameof(IdentityController),
                    nameof(Refresh),
                    authResponse.Errors);
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors,
                });
            }

            _logger.LogInformationWithProjectTemplate(
                nameof(IdentityController),
                nameof(Refresh),
                "Refresh successfully");

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken,
            });
        }

        [HttpPost(ApiRoutes.Identity.Logout)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenInfo refreshToken)
        {
            var authResponse = await _tokenService.RevokeTokenAsync(refreshToken.RefreshToken);

            if (!authResponse.Success)
            {
                _logger.LogInformationWithProjectTemplate(
                    nameof(IdentityController),
                    nameof(Refresh),
                    authResponse.Errors);
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors,
                });
            }

            _logger.LogInformationWithProjectTemplate(
                nameof(IdentityController),
                nameof(Refresh),
                "Revoke token successfully");

            return Ok();
        }

        [HttpPost(ApiRoutes.Identity.ChangePassword)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> ChangePassword([FromBody] string refreshToken)
        {
            var authResponse = await _tokenService.RevokeTokenAsync(refreshToken);

            if (!authResponse.Success)
            {
                _logger.LogInformationWithProjectTemplate(
                    nameof(IdentityController),
                    nameof(Refresh),
                    authResponse.Errors);
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors,
                });
            }

            _logger.LogInformationWithProjectTemplate(
                nameof(IdentityController),
                nameof(Refresh),
                "Revoke token successfully");

            return Ok();
        }

        [HttpGet(ApiRoutes.Identity.ActivateUser)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> ActivateUser([FromRoute] string id)
        {
            var authResponse = await _identityService.ActivateUserAsync(id);

            if (!authResponse.Success)
            {
                _logger.LogInformationWithProjectTemplate(
                    nameof(IdentityController),
                    nameof(ActivateUser),
                    authResponse.Errors);
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors,
                });
            }

            _logger.LogInformationWithProjectTemplate(
                nameof(IdentityController),
                nameof(ActivateUser),
                $"Activate user {id} successfully");

            return Ok();
        }

        [HttpGet(ApiRoutes.Identity.DeactivateUser)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> DeactivateUser([FromRoute] string id)
        {
            var authResponse = await _identityService.DeactivateUser(id);

            if (!authResponse.Success)
            {
                _logger.LogInformationWithProjectTemplate(
                    nameof(IdentityController),
                    nameof(DeactivateUser),
                    authResponse.Errors);
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors,
                });
            }

            _logger.LogInformationWithProjectTemplate(
                nameof(IdentityController),
                nameof(DeactivateUser),
                $"Deactivate user {id} successfully");

            return Ok();
        }
    }
}
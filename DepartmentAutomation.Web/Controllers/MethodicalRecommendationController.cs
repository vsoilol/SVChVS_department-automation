using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.MethodicalRecommendations.Commands.CreateMethodicalRecommendation;
using DepartmentAutomation.Application.Features.MethodicalRecommendations.Commands.DeleteMethodicalRecommendation;
using DepartmentAutomation.Application.Features.MethodicalRecommendations.Commands.UpdateMethodicalRecommendation;
using DepartmentAutomation.Application.Features.MethodicalRecommendations.Queries.
    GetMethodicalRecommendationsByProgramId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class MethodicalRecommendationController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.MethodicalRecommendation.GetMethodicalRecommendationByProgramId)]
        public async Task<ActionResult<List<MethodicalRecommendationDto>>> GetMethodicalRecommendationByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetMethodicalRecommendationsByProgramIdQuery
            { EducationalProgramId = educationalProgramId });
        }

        [HttpPost(ApiRoutes.MethodicalRecommendation.Base)]
        public async Task<ActionResult<int>> CreateMethodicalRecommendationAsync(
            [FromBody] CreateMethodicalRecommendationCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete(ApiRoutes.MethodicalRecommendation.BaseWithId)]
        public async Task<ActionResult> DeleteMethodicalRecommendationAsync(
            [FromRoute] int id)
        {
            await Mediator.Send(new DeleteMethodicalRecommendationCommand { Id = id });
            return NoContent();
        }

        [HttpPut(ApiRoutes.MethodicalRecommendation.Base)]
        public async Task<ActionResult> UpdateMethodicalRecommendationAsync(
            [FromBody] UpdateMethodicalRecommendationCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
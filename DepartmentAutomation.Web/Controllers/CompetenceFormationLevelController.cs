using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.CompetenceFormationLevels.Commands.UpdateCompetenceFormationLevel;
using DepartmentAutomation.Application.Features.CompetenceFormationLevels.Queries.
    GetCompetenceFormationLevelByCompetence;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class CompetenceFormationLevelController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.CompetenceFormationLevel.GetCompetenceFormationLevelByCompetenceId)]
        public async Task<ActionResult<List<CompetenceFormationLevelDto>>>
            GetCompetenceFormationLevelByCompetenceIdAsync(
                [FromQuery] GetCompetenceFormationLevelByCompetenceQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut(ApiRoutes.CompetenceFormationLevel.Base)]
        public async Task<ActionResult> UpdateCompetenceFormationLevelAsync(
            [FromBody] UpdateCompetenceFormationLevelCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Weeks.Commands.UpdateWeek;
using DepartmentAutomation.Application.Features.Weeks.Queries.GetByModuleNumber;
using DepartmentAutomation.Application.Features.Weeks.Queries.GetTrainingModuleNumbers;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class WeekController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Week.GetTrainingModuleNumbers)]
        public async Task<ActionResult<List<int>>> GetTrainingModuleNumbersAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetTrainingModuleNumbersQuery { EducationalProgramId = educationalProgramId });
        }

        [HttpGet(ApiRoutes.Week.GetWeeksByModuleNumber)]
        public async Task<ActionResult<List<WeekDto>>> GetWeeksByModuleNumberAsync(
            [FromQuery] GetWeeksByModuleNumberQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut(ApiRoutes.Week.Base)]
        public async Task<ActionResult> UpdateAsync(
            [FromBody] UpdateWeekCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
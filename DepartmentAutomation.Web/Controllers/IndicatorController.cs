using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Indicators.Queries.GetIndicatorWithLevelsByProgramId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher, Role.Admin)]
    public class IndicatorController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Indicator.GetIndicatorWithLevelsByProgramId)]
        public async Task<ActionResult<List<IndicatorWithLevelsDto>>> GetIndicatorWithLevelsByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetIndicatorWithLevelsByProgramIdQuery
                { EducationalProgramId = educationalProgramId });
        }
    }
}
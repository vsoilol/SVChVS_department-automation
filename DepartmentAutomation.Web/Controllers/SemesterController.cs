using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Semesters.Queries.GetAllSemestersByProgramId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class SemesterController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Semester.GetAllSemestersByProgramId)]
        public async Task<ActionResult<List<SemesterDto>>> GetAllSemestersByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetAllSemestersByProgramIdQuery { EducationalProgramId = educationalProgramId });
        }
    }
}
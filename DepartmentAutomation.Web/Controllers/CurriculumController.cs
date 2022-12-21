using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Curriculums.Queries.GetAllYearsByDepartmentId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CurriculumController : ApiControllerBase
    {
        [AuthorizeRoles(Role.DepartmentHead)]
        [HttpGet(ApiRoutes.Curriculum.GetAllYearsByDepartmentId)]
        public async Task<ActionResult<List<CurriculumsStudyStartingYearDto>>>
            GetAllYearsByDepartmentIdAsync([FromRoute] int departmentId)
        {
            return await Mediator.Send(new GetAllYearsByDepartmentIdQuery{DepartmentId = departmentId});
        }
    }
}
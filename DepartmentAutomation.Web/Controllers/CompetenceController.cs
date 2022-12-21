using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Competences.Queries.GetAllCompetencesByProgramId;
using DepartmentAutomation.Application.Features.Competences.Queries.GetCompetencesInfoByLessonId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompetenceController : ApiControllerBase
    {
        [AuthorizeRoles(Role.Teacher)]
        [HttpGet(ApiRoutes.Competence.GetByLessonId)]
        public async Task<ActionResult<List<CompetenceDto>>> GetByLessonIdAsync(
            [FromRoute] int lessonId)
        {
            return await Mediator.Send(new GetByLessonIdQuery
            { LessonId = lessonId });
        }

        [AuthorizeRoles(Role.Teacher)]
        [HttpGet(ApiRoutes.Competence.GetAllByProgramId)]
        public async Task<ActionResult<List<CompetenceDto>>> GetAllByProgramId(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetAllCompetencesByProgramIdQuery
            { EducationalProgramId = educationalProgramId });
        }
    }
}
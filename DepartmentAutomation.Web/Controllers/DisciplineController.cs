using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Application.Contracts.Requests;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.Features.Disciplines.Commands.ChangeDisciplineStatus;
using DepartmentAutomation.Application.Features.Disciplines.Commands.UpdateDisciplineTeachers;
using DepartmentAutomation.Application.Features.Disciplines.Queries.GetAdditionalDisciplineInfoById;
using DepartmentAutomation.Application.Features.Disciplines.Queries.GetBriefDisciplineInfo;
using DepartmentAutomation.Application.Features.Disciplines.Queries.GetDisciplinesWithFiltersByDepartmentId;
using DepartmentAutomation.Application.Features.Disciplines.Queries.IsEducationalProgramExist;
using DepartmentAutomation.Application.Features.EducationalPrograms.Commands.ChangeProgramStatus;
using DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetProgramsByUserIdWithPagination;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetAllTeachers;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DisciplineController : ApiControllerBase
    {
        /// <summary>
        /// Return all diciplines from database
        /// </summary>
        /// <response code="200">Return all diciplines from database</response>
        [AuthorizeRoles(Role.Admin)]
        [HttpGet(ApiRoutes.Discipline.Base)]
        public async Task<ActionResult<List<TeacherBriefDto>>> GetAllAsync()
        {
            return await Mediator.Send(new GetAllTeachersQuery());
        }

        [AuthorizeRoles(Role.Teacher)]
        [HttpGet(ApiRoutes.Discipline.GetBriefInfo)]
        public async Task<ActionResult<DisciplineBriefDto>> GetBriefInfoAsync([FromRoute] int id)
        {
            return await Mediator.Send(new GetBriefDisciplineInfoQuery { DisciplineId = id });
        }

        [AuthorizeRoles(Role.DepartmentHead)]
        [HttpGet(ApiRoutes.Discipline.GetWithPagination)]
        public async Task<ActionResult<PaginatedList<DepartmentHeadDisciplineDto>>>
            GetDisciplinesWithFiltersByDepartmentIdAsync(
                [FromQuery] PaginationRequest paginationRequest,
                [FromQuery] DisciplinesFilterDto filterDto)
        {
            var query = new GetDisciplinesWithFiltersByDepartmentIdQuery(paginationRequest, filterDto);
            return await Mediator.Send(query);
        }

        [AuthorizeRoles(Role.DepartmentHead)]
        [HttpGet(ApiRoutes.Discipline.GetAdditionalDisciplineInfoById)]
        public async Task<ActionResult<AdditionalDisciplineInfoDto>>
            GetAdditionalDisciplineInfoByIdAsync([FromRoute] int disciplineId)
        {
            var query = new GetAdditionalDisciplineInfoByIdQuery { DisciplineId = disciplineId };
            return await Mediator.Send(query);
        }

        [AuthorizeRoles(Role.DepartmentHead)]
        [HttpPut(ApiRoutes.Discipline.UpdateDisciplineTeachers)]
        public async Task<ActionResult> UpdateDisciplineTeachersAsync(
            [FromBody] UpdateDisciplineTeachersCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        [AuthorizeRoles(Role.DepartmentHead)]
        [HttpPut(ApiRoutes.Discipline.ChangeStatus)]
        public async Task<ActionResult> ChangeStatusAsync([FromBody] ChangeDisciplineStatusCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        [AuthorizeRoles(Role.DepartmentHead)]
        [HttpGet(ApiRoutes.Discipline.IsEducationalProgramExist)]
        public async Task<ActionResult> IsEducationalProgramExistAsync([FromRoute] int id)
        {
            return Ok(await Mediator.Send(new IsEducationalProgramExistQuery{Id = id}));
        }
    }
}
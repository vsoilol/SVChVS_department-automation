using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Application.Contracts.Requests;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.Contracts.Responses.Common;
using DepartmentAutomation.Application.Features.Teachers.Commands.ChangeTeacherPassword;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetAllTeachers;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersByDepartmentId;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersFullNameByDisciplineId;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersWithPagination;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeacherController : ApiControllerBase
    {
        /// <summary>
        /// Return all teachers from database
        /// </summary>
        /// <response code="200">Return all teachers from database</response>
        [AuthorizeRoles(Role.Admin)]
        [HttpGet(ApiRoutes.Teacher.Base)]
        public async Task<ActionResult<List<TeacherBriefDto>>> GetAll()
        {
            return await Mediator.Send(new GetAllTeachersQuery());
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpGet(ApiRoutes.Teacher.GetWithPagination)]
        public async Task<ActionResult<PaginatedList<TeacherBriefDto>>> GetTeachersWithPagination(
            [FromQuery] PaginationRequest paginationRequest, [FromQuery] TeacherFilterDto teacherFilterDto)
        {
            var query = new GetTeachersWithPaginationQuery(paginationRequest, teacherFilterDto);
            return await Mediator.Send(query);
        }
        
        [AuthorizeRoles(Role.DepartmentHead)]
        [HttpGet(ApiRoutes.Teacher.GetTeachersByDepartmentId)]
        public async Task<ActionResult<List<TeacherFullNameDto>>> GetTeachersByDepartmentIdAsync(
            [FromRoute] int departmentId)
        {
            var query = new GetTeachersByDepartmentIdQuery {DepartmentId = departmentId};
            return await Mediator.Send(query);
        }
        
        [AuthorizeRoles(Role.DepartmentHead)]
        [HttpGet(ApiRoutes.Teacher.GetTeachersFullNameByDisciplineId)]
        public async Task<ActionResult<List<TeacherFullNameDto>>> GetTeachersFullNameByDisciplineIdAsync(
            [FromRoute] int disciplineId)
        {
            var query = new GetTeachersFullNameByDisciplineIdQuery() {DisciplineId = disciplineId};
            return await Mediator.Send(query);
        }

        [AuthorizeRoles(Role.Admin)]
        [HttpPost(ApiRoutes.Teacher.ChangeTeacherPassword)]
        public async Task<ActionResult<NewPasswordDto>> ChangeTeacherPassword(
            [FromBody] ChangeTeacherPasswordCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
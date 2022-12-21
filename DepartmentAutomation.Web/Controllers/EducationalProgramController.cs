using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Application.Contracts.Requests;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.Features.EducationalPrograms.Commands.ChangeProgramStatus;
using DepartmentAutomation.Application.Features.EducationalPrograms.Commands.CreateDefaultProgram;
using DepartmentAutomation.Application.Features.EducationalPrograms.Commands.DeleteProgram;
using DepartmentAutomation.Application.Features.EducationalPrograms.Commands.UpdateAudiences;
using DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetEducationalProgramById;
using DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetProgramsByUserIdWithPagination;
using DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetProgramWordDocument;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Shared.Constants;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher, Role.DepartmentHead, Role.Admin)]
    public class EducationalProgramController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.EducationalProgram.BaseWithId)]
        public async Task<ActionResult<EducationalProgramDto>> GetByIdAsync(
            [FromRoute] int id)
        {
            return await Mediator.Send(new GetEducationalProgramByIdQuery { Id = id });
        }

        [HttpDelete(ApiRoutes.EducationalProgram.BaseWithId)]
        public async Task<ActionResult> DeleteAsync(
            [FromRoute] int id)
        {
            await Mediator.Send(new DeleteProgramCommand { EducationalProgramId = id });
            return NoContent();
        }

        [HttpGet(ApiRoutes.EducationalProgram.DownloadWordDocument)]
        public async Task<ActionResult> DownloadWordDocumentAsync([FromRoute] int id)
        {
            var bytes = await Mediator.Send(new GetProgramWordDocumentQuery { EducationalProgramId = id });
            return File(bytes, ContentTypes.Word);
        }

        [HttpGet(ApiRoutes.EducationalProgram.GetWithPagination)]
        public async Task<ActionResult<PaginatedList<EducationalProgramBriefDto>>>
            GetAllByTeacherIdWithPagination(
                [FromQuery] PaginationRequest paginationRequest,
                [FromQuery] EducationalProgramsFilterDto filterDto)
        {
            var query = new GetProgramsByUserIdWithPaginationQuery(paginationRequest, filterDto);
            return await Mediator.Send(query);
        }

        [HttpPost(ApiRoutes.EducationalProgram.CreateDefault)]
        public async Task<ActionResult> CreateDefaultProgramAsync(
            [FromRoute] int disciplineId)
        {
            await Mediator.Send(new CreateDefaultProgramCommand { DisciplineId = disciplineId });
            return NoContent();
        }

        [HttpPut(ApiRoutes.EducationalProgram.UpdateAudiencesInfo)]
        public async Task<ActionResult> UpdateAudiencesInfoAsync(
            [FromBody] UpdateAudiencesCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut(ApiRoutes.EducationalProgram.ChangeStatus)]
        public async Task<ActionResult> ChangeStatusAsync([FromBody] ChangeProgramStatusCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Literatures.Commands.CreateLiterature;
using DepartmentAutomation.Application.Features.Literatures.Commands.DeleteLiterature;
using DepartmentAutomation.Application.Features.Literatures.Commands.UpdateLiterature;
using DepartmentAutomation.Application.Features.Literatures.Queries.GetLiteraturesByType;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class LiteratureController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Literature.GetLiteraturesByType)]
        public async Task<ActionResult<List<LiteratureDto>>> GetLiteraturesByTypeAsync(
            [FromQuery] GetLiteraturesByTypeQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost(ApiRoutes.Literature.Base)]
        public async Task<ActionResult<int>> CreateLiteraturesAsync(
            [FromBody] CreateLiteratureCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete(ApiRoutes.Literature.BaseWithId)]
        public async Task<ActionResult> DeleteLiteraturesAsync(
            [FromRoute] int id)
        {
            await Mediator.Send(new DeleteLiteratureCommand { LiteratureId = id });
            return NoContent();
        }

        [HttpPut(ApiRoutes.Literature.Base)]
        public async Task<ActionResult> UpdateLiteraturesAsync(
            [FromBody] UpdateLiteratureCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
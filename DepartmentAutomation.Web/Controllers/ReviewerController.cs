using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Reviewers.Commands.UpdateReviewer;
using DepartmentAutomation.Application.Features.Reviewers.Queries.GetReviewerByProgramId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class ReviewerController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Reviewer.GetReviewerByProgramId)]
        public async Task<ActionResult<ReviewerDto>> GetReviewerByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetReviewerByProgramIdQuery { EducationalProgramId = educationalProgramId });
        }

        [HttpPut(ApiRoutes.Reviewer.Base)]
        public async Task<ActionResult> UpdateReviewerAsync(
            [FromBody] UpdateReviewerCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.Features.EvaluationTools.Commands.CreateEvaluationTool;
using DepartmentAutomation.Application.Features.EvaluationTools.Commands.DeleteEvaluationTool;
using DepartmentAutomation.Application.Features.EvaluationTools.Queries.GetAllEvaluationToolByProgramId;
using DepartmentAutomation.Application.Features.EvaluationTools.Queries.GetEvaluationToolTypeByProgramId;
using DepartmentAutomation.Application.Features.EvaluationTools.Queries.GetNotChoosenEvaluationTool;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class EvaluationToolController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.EvaluationTool.GetNotChoosenEvaluationTool)]
        public async Task<ActionResult<List<EvaluationToolTypeDto>>> GetNotChoosenEvaluationToolAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetNotChoosenEvaluationToolQuery
            { EducationalProgramId = educationalProgramId });
        }

        [HttpGet(ApiRoutes.EvaluationTool.GetAllEvaluationToolByProgramId)]
        public async Task<ActionResult<List<EvaluationToolBriefDto>>> GetAllEvaluationToolByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetAllEvaluationToolByProgramIdQuery
            { EducationalProgramId = educationalProgramId });
        }

        [HttpPost(ApiRoutes.EvaluationTool.Base)]
        public async Task<ActionResult> CreateEvaluationToolAsync([FromBody] CreateEvaluationToolCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.EvaluationTool.Base)]
        public async Task<ActionResult> DeleteEvaluationToolAsync([FromQuery] DeleteEvaluationToolCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet(ApiRoutes.EvaluationTool.GetAllEvaluationToolTypeByProgramId)]
        public async Task<ActionResult<List<EvaluationToolTypeDto>>> GetAllEvaluationToolTypeByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetEvaluationToolTypeByProgramIdQuery
            { EducationalProgramId = educationalProgramId });
        }
    }
}
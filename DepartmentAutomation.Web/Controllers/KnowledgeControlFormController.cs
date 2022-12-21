using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.KnowledgeControlForms.Queries.GetAllKnowledgeControlForm;
using DepartmentAutomation.Application.Features.KnowledgeControlForms.Queries.GetKnowledgeControlFormsByWeekId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class KnowledgeControlFormController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.KnowledgeControlForm.GetAllByWeekId)]
        public async Task<ActionResult<List<KnowledgeAssessmentDto>>> GetAllByWeekIdAsync(
            [FromRoute] int weekId)
        {
            return await Mediator.Send(new GetKnowledgeControlFormsByWeekIdQuery { WeekId = weekId });
        }

        [HttpGet(ApiRoutes.KnowledgeControlForm.Base)]
        public async Task<ActionResult<List<KnowledgeControlFormDto>>> GetAllAsync()
        {
            return await Mediator.Send(new GetAllKnowledgeControlFormQuery());
        }
    }
}
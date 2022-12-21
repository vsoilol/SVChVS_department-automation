using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.Features.Audiences.Queries.GetAllAudience;
using DepartmentAutomation.Application.Features.Audiences.Queries.GetAllAudiencesByProgramId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartmentAutomation.Web.Controllers
{

    public class AudienceController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Audience.GetAllAudiencesByProgramId)]
        public async Task<ActionResult<List<AudienceBriefInfoDto>>> GetAllAudiencesByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetAllAudiencesByProgramIdQuery { EducationalProgramId = educationalProgramId });
        }
        
        [HttpGet(ApiRoutes.Audience.Base)]
        public async Task<ActionResult<List<AudienceBriefInfoDto>>> GetAllAudiencesAsync()
        {
            return await Mediator.Send(new GetAllAudienceQuery());
        }
    }
}
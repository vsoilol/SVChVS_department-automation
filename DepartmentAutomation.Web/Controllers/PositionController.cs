using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Positions.Queries.GetAllPositions;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Admin)]
    public class PositionController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Position.GetAll)]
        public async Task<ActionResult<List<PositionDto>>> GetAll()
        {
            return await Mediator.Send(new GetAllPositionsQuery());
        }
    }
}
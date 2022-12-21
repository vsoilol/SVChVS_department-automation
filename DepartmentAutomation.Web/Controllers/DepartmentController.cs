using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.Departments.Queries.GetAllDepartments;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Admin)]
    public class DepartmentController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Department.GetAll)]
        public async Task<ActionResult<List<DepartmentDto>>> GetAll()
        {
            return await Mediator.Send(new GetAllDepartmentsQuery());
        }
    }
}
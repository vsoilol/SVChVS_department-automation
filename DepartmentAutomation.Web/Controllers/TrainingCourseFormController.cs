using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Features.TrainingCourseForms.Commands.AddLessonsToTrainingCourseForm;
using DepartmentAutomation.Application.Features.TrainingCourseForms.Commands.DeleteLessonsFromTrainingCourseForm;
using DepartmentAutomation.Application.Features.TrainingCourseForms.Queries.GetAllTrainingCourseFormByProgramId;
using DepartmentAutomation.Application.Features.TrainingCourseForms.Queries.GetAllTrainingCourseFormWithoutLessons;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class TrainingCourseFormController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.TrainingCourseForm.GetAllByProgramId)]
        public async Task<ActionResult<List<TrainingCourseFormDto>>> GetAllByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetAllTrainingCourseFormByProgramIdQuery { EducationalProgramId = educationalProgramId });
        }

        [HttpGet(ApiRoutes.TrainingCourseForm.GetAllWithoutLessons)]
        public async Task<ActionResult<List<TrainingCourseFormDto>>> GetAllWithoutLessonsAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetAllTrainingCourseFormWithoutLessonsQuery { EducationalProgramId = educationalProgramId });
        }

        [HttpPost(ApiRoutes.TrainingCourseForm.AddLessonsToTrainingCourseForm)]
        public async Task<ActionResult> AddLessonsToTrainingCourseFormAsync(
            [FromBody] AddLessonsToTrainingCourseFormQuery query)
        {
            await Mediator.Send(query);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.TrainingCourseForm.DeleteLessonsFromTrainingCourseForm)]
        public async Task<ActionResult> DeleteLessonsFromTrainingCourseFormAsync(
            [FromQuery] DeleteLessonsFromTrainingCourseFormQuery query)
        {
            await Mediator.Send(query);
            return NoContent();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Attributes;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.Features.Lessons.Commands.CreateLesson;
using DepartmentAutomation.Application.Features.Lessons.Commands.DeleteLesson;
using DepartmentAutomation.Application.Features.Lessons.Commands.UpdateLesson;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLecturesByProgramId;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLessonsByProgramId;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLessonsWithoutTrainingCourseForm;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLessonsWithoutWeek;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetLectureById;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetLessonByWeekId;
using DepartmentAutomation.Domain.Enums;
using DepartmentAutomation.Web.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentAutomation.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AuthorizeRoles(Role.Teacher)]
    public class LessonController : ApiControllerBase
    {
        [HttpGet(ApiRoutes.Lesson.GetAllLessonsByProgramId)]
        public async Task<ActionResult<List<LessonDto>>> GetAllLessonsByProgramIdAsync(
            [FromQuery] GetAllLessonsByProgramIdQuery query)
        {
            return await Mediator.Send(query);
        }
        
        [HttpGet(ApiRoutes.Lesson.GetAllLescturesByProgramId)]
        public async Task<ActionResult<List<LecturesBriefDto>>> GetAllLescturesByProgramIdAsync(
            [FromRoute] int educationalProgramId)
        {
            return await Mediator.Send(new GetAllLecturesByProgramIdQuery{EducationalProgramId = educationalProgramId});
        }
        
        [HttpGet(ApiRoutes.Lesson.GetLectureById)]
        public async Task<ActionResult<LectureDto>> GetLesctureByIdAsync(
            [FromRoute] int id)
        {
            return await Mediator.Send(new GetLectureByIdQuery(){Id = id});
        }

        [HttpGet(ApiRoutes.Lesson.GetLessonByWeekId)]
        public async Task<ActionResult<LessonDto>> GetLessonByWeekIdAsync(
            [FromQuery] GetLessonByWeekIdQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost(ApiRoutes.Lesson.Base)]
        public async Task<ActionResult<int>> CreateLessonAsync([FromBody] CreateLessonCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut(ApiRoutes.Lesson.Base)]
        public async Task<ActionResult> UpdateLessonAsync([FromBody] UpdateLessonCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete(ApiRoutes.Lesson.BaseWithId)]
        public async Task<ActionResult> DeleteLessonAsync([FromRoute] int id)
        {
            await Mediator.Send(new DeleteLessonCommand { LessonId = id });
            return NoContent();
        }

        [HttpGet(ApiRoutes.Lesson.GetAllLessonsWithoutWeek)]
        public async Task<ActionResult<List<LessonDto>>> GetAllLessonsWithoutWeekAsync(
            [FromQuery] GetAllLessonsWithoutWeekQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet(ApiRoutes.Lesson.GetAllLessonsWithoutTrainingCourseForm)]
        public async Task<ActionResult<List<LessonDto>>> GetAllLessonsWithoutTrainingCourseFormAsync(
            [FromQuery] GetAllLessonsWithoutTrainingCourseFormQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
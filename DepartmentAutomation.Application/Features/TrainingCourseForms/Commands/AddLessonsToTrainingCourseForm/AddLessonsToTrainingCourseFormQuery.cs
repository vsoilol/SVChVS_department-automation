using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.TrainingCourseForms.Commands.AddLessonsToTrainingCourseForm
{
    public class AddLessonsToTrainingCourseFormQuery : IRequest
    {
        public int TrainingCourseFormId { get; set; }

        public int EducationalProgramId { get; set; }

        public LessonType LessonType { get; set; }

        public int FromLessonId { get; set; }

        public int ToLessonId { get; set; }
    }

    public class AddLessonsToTrainingCourseFormQueryHandler : IRequestHandler<AddLessonsToTrainingCourseFormQuery>
    {
        private readonly IApplicationDbContext _context;

        public AddLessonsToTrainingCourseFormQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddLessonsToTrainingCourseFormQuery request, CancellationToken cancellationToken)
        {
            var lessons = _context.Lessons.Where(_ =>
                    _.LessonType == request.LessonType
                    && _.EducationalProgramId == request.EducationalProgramId)
                .OrderBy(_ => _.Id)
                .Where(_ => _.Id >= request.FromLessonId && _.Id <= request.ToLessonId);

            var trainingCourseForm = await _context.TrainingCourseForms
                .Include(_ => _.Lessons)
                .Include(_ => _.EducationalPrograms)
                .FirstOrDefaultAsync(_ => _.Id == request.TrainingCourseFormId,
                    cancellationToken: cancellationToken);

            var educationalProgram = await _context.EducationalPrograms
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId,
                    cancellationToken: cancellationToken);

            trainingCourseForm.Lessons.AddRange(await lessons.ToListAsync(cancellationToken: cancellationToken));
            trainingCourseForm.EducationalPrograms.Add(educationalProgram);

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
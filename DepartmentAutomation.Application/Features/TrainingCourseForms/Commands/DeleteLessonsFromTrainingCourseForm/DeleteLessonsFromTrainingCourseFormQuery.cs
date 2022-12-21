using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.TrainingCourseForms.Commands.DeleteLessonsFromTrainingCourseForm
{
    public class DeleteLessonsFromTrainingCourseFormQuery : IRequest
    {
        public int EducationalProgramId { get; set; }

        public int TrainingCourseFormId { get; set; }
    }

    public class
        DeleteLessonsFromTrainingCourseFormQueryHandler : IRequestHandler<DeleteLessonsFromTrainingCourseFormQuery>
    {
        private readonly IApplicationDbContext _context;

        public DeleteLessonsFromTrainingCourseFormQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteLessonsFromTrainingCourseFormQuery request,
            CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            var trainingCourseForm = await _context.TrainingCourseForms
                .Include(_ => _.Lessons)
                .Include(_ => _.EducationalPrograms)
                .FirstOrDefaultAsync(_ => _.Id == request.TrainingCourseFormId, cancellationToken: cancellationToken);

            await _context.Lessons
                .Where(_ => _.EducationalProgramId == request.EducationalProgramId
                            && _.TrainingCourseFormId == request.TrainingCourseFormId)
                .ForEachAsync(_ => { _.TrainingCourseForm = null; }, cancellationToken: cancellationToken);

            trainingCourseForm.EducationalPrograms.Remove(educationalProgram);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
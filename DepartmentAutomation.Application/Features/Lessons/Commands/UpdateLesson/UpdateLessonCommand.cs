using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonCommand : IRequest
    {
        public LessonDto Lesson { get; set; }

        public LessonType LessonType { get; set; }

        public int[] CompetencesId { get; set; }
    }

    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateLessonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _context.Lessons.Include(_ => _.Competences).FirstOrDefaultAsync(_ => _.Id == request.Lesson.Id,
                cancellationToken: cancellationToken);

            if (request.LessonType == LessonType.Lecture)
            {
                var competences =
                    _context.Competences
                        .Where(_ => request.CompetencesId.Any(competenceId => competenceId == _.Id));

                lesson.Competences = await competences.ToListAsync(cancellationToken: cancellationToken);
                lesson.Content = request.Lesson.Content;
            }

            lesson.Hours = request.Lesson.Hours;
            lesson.Name = request.Lesson.Name;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
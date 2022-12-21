using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommand : IRequest<int>
    {
        public LessonDto Lesson { get; set; }

        public LessonType LessonType { get; set; }

        public int[] CompetencesId { get; set; }

        public int EducationalProgramId { get; set; }
    }

    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateLessonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lessonsCount = _context.Lessons
                .Count(_ => _.LessonType == request.LessonType &&
                            _.EducationalProgramId == request.EducationalProgramId);

            var lesson = new Lesson
            {
                Hours = request.Lesson.Hours,
                Number = lessonsCount + 1,
                LessonType = request.LessonType,
                Name = request.Lesson.Name,
                EducationalProgramId = request.EducationalProgramId
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.LessonType == LessonType.Lecture)
            {
                lesson.Competences = new List<Competence>();
                lesson.Content = request.Lesson.Content;

                var competences =
                    _context.Competences
                        .Where(_ => request.CompetencesId.Any(competenceId => competenceId == _.Id));


                lesson.Competences.AddRange(competences);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return lesson.Id;
        }
    }
}
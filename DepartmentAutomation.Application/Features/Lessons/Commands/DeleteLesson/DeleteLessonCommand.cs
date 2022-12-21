using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommand : IRequest
    {
        public int LessonId { get; set; }
    }

    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteLessonCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(_ => _.Id == request.LessonId,
                cancellationToken: cancellationToken);
            var number = lesson.Number;
            await _context.Lessons.Where(_ => _.LessonType == lesson.LessonType && _.Id > lesson.Id)
                .ForEachAsync(_ => { _.Number = number++; }, cancellationToken: cancellationToken);

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
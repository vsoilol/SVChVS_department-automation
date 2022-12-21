using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Domain.Enums;

namespace DepartmentAutomation.Application.Features.EducationalPrograms.Commands.DeleteProgram
{
    public class DeleteProgramCommand : IRequest
    {
        public int EducationalProgramId { get; set; }
    }

    public class DeleteProgramCommandHandler : IRequestHandler<DeleteProgramCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProgramCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.Discipline)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            educationalProgram.Discipline.Status = Status.NotExist;
            
            var lessons = _context.Lessons.Where(_ => _.EducationalProgramId == request.EducationalProgramId);
            _context.Lessons.RemoveRange(lessons);

            _context.EducationalPrograms.Remove(educationalProgram);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
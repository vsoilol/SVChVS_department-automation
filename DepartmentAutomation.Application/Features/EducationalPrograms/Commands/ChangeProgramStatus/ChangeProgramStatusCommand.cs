using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.EducationalPrograms.Commands.ChangeProgramStatus
{
    public class ChangeProgramStatusCommand : IRequest
    {
        public int Id { get; set; }

        public Status Status { get; set; }
    }

    public class ChangeProgramStatusCommandHandler : IRequestHandler<ChangeProgramStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public ChangeProgramStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ChangeProgramStatusCommand request, CancellationToken cancellationToken)
        {
            var educationalProgram =
                await _context.EducationalPrograms
                    .Include(_ => _.Discipline)
                    .FirstOrDefaultAsync(_ => _.Id == request.Id,
                    cancellationToken: cancellationToken);

            educationalProgram.Discipline.Status = request.Status;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
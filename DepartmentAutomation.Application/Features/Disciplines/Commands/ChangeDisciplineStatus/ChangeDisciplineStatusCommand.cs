using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Disciplines.Commands.ChangeDisciplineStatus
{
    public class ChangeDisciplineStatusCommand : IRequest
    {
        public int Id { get; set; }

        public Status Status { get; set; }
    }
    
    public class ChangeDisciplineStatusCommandHandler : IRequestHandler<ChangeDisciplineStatusCommand>
    {
        private readonly IApplicationDbContext _context;

        public ChangeDisciplineStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ChangeDisciplineStatusCommand request, CancellationToken cancellationToken)
        {
            var discipline =
                await _context.Disciplines
                    .FirstOrDefaultAsync(_ => _.Id == request.Id,
                        cancellationToken: cancellationToken);

            discipline.Status = request.Status;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
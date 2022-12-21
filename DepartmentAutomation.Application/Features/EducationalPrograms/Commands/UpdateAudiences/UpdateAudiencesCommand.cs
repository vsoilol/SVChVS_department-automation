using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.EducationalPrograms.Commands.UpdateAudiences
{
    public class UpdateAudiencesCommand : IRequest
    {
        public int EducationalProgramId { get; set; }

        public int[] AudienceIds { get; set; }
    }

    public class UpdateAudiencesCommandHandler : IRequestHandler<UpdateAudiencesCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAudiencesCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAudiencesCommand request, CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.Audiences)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            var audiences = await _context.Audiences
                .Where(_ => request.AudienceIds.Any(id => _.Id == id))
                .ToListAsync(cancellationToken: cancellationToken);

            educationalProgram.Audiences = audiences;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
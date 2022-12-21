using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.InformationBlocks.Commands.DeleteInformationBlockContent
{
    public class DeleteInformationBlockContentCommand : IRequest
    {
        public int EducationalProgramId { get; set; }

        public int InformationBlockId { get; set; }
    }

    public class DeleteInformationBlockContentCommandHandler : IRequestHandler<DeleteInformationBlockContentCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteInformationBlockContentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInformationBlockContentCommand request, CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.InformationBlockContents)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            educationalProgram.InformationBlockContents
                .RemoveAll(_ => _.InformationBlockId == request.InformationBlockId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
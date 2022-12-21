using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;

namespace DepartmentAutomation.Application.Features.InformationBlocks.Commands.CreateInformationBlockContent
{
    public class CreateInformationBlockContentCommand : IRequest
    {
        public int EducationalProgramId { get; set; }

        public int InformationBlockId { get; set; }
    }

    public class CreateInformationBlockContentCommandHandler : IRequestHandler<CreateInformationBlockContentCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateInformationBlockContentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateInformationBlockContentCommand request, CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.InformationBlockContents)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            educationalProgram.InformationBlockContents.Add(new InformationBlockContent
            {
                EducationalProgram = educationalProgram,
                InformationBlockId = request.InformationBlockId,
                Content = "",
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
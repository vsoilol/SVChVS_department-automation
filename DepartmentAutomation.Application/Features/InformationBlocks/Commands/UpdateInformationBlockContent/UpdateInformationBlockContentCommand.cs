using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;

namespace DepartmentAutomation.Application.Features.InformationBlocks.Commands.UpdateInformationBlockContent
{
    public class UpdateInformationBlockContentCommand : IRequest
    {
        public int EducationalProgramId { get; set; }

        public int InformationBlockId { get; set; }

        public string Content { get; set; }
    }

    public class UpdateInformationBlockContentCommandHandler : IRequestHandler<UpdateInformationBlockContentCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateInformationBlockContentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateInformationBlockContentCommand request,
            CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.InformationBlockContents)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            educationalProgram.InformationBlockContents
                .RemoveAll(_ => _.InformationBlockId == request.InformationBlockId);

            educationalProgram.InformationBlockContents.Add(new InformationBlockContent
            {
                EducationalProgram = educationalProgram,
                InformationBlockId = request.InformationBlockId,
                Content = request.Content
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
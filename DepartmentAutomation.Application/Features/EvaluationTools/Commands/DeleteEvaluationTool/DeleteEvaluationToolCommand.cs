using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.EvaluationTools.Commands.DeleteEvaluationTool
{
    public class DeleteEvaluationToolCommand : IRequest
    {
        public int EducationalProgramId { get; set; }

        public int EvaluationToolTypeId { get; set; }
    }

    public class DeleteEvaluationToolCommandHandler : IRequestHandler<DeleteEvaluationToolCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEvaluationToolCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEvaluationToolCommand request, CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.EvaluationTools)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            educationalProgram.EvaluationTools
                .RemoveAll(_ => _.EvaluationToolTypeId == request.EvaluationToolTypeId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
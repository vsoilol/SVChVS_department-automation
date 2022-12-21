using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;

namespace DepartmentAutomation.Application.Features.EvaluationTools.Commands.CreateEvaluationTool
{
    public class CreateEvaluationToolCommand : IRequest
    {
        public int EducationalProgramId { get; set; }

        public int EvaluationToolTypeId { get; set; }

        public int SetNumber { get; set; }
    }

    public class CreateEvaluationToolCommandHandler : IRequestHandler<CreateEvaluationToolCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateEvaluationToolCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateEvaluationToolCommand request, CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.EvaluationTools)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            educationalProgram.EvaluationTools.Add(new EvaluationTool
            {
                EducationalProgram = educationalProgram,
                EvaluationToolTypeId = request.EvaluationToolTypeId,
                SetNumber = request.SetNumber,
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
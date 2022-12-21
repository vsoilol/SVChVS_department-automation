using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.CompetenceFormationLevels.Commands.UpdateCompetenceFormationLevel
{
    public class UpdateCompetenceFormationLevelCommand : IRequest
    {
        public int Id { get; set; }

        public string FactualDescription { get; set; }

        public string LearningOutcomes { get; set; }

        public List<int> EvaluationToolTypeIds { get; set; }
    }

    public class UpdateCompetenceFormationLevelCommandHandler : IRequestHandler<UpdateCompetenceFormationLevelCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCompetenceFormationLevelCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCompetenceFormationLevelCommand request, CancellationToken cancellationToken)
        {
            var competenceFormationLevel = await _context.CompetenceFormationLevels
                .Include(_ => _.EvaluationToolTypes)
                .FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

            competenceFormationLevel.FactualDescription = request.FactualDescription;
            competenceFormationLevel.LearningOutcomes = request.LearningOutcomes;

            var evaluationToolTypes = await _context.EvaluationToolTypes
                .Where(_ => request.EvaluationToolTypeIds.Contains(_.Id))
                .ToListAsync(cancellationToken: cancellationToken);

            competenceFormationLevel.EvaluationToolTypes = evaluationToolTypes;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
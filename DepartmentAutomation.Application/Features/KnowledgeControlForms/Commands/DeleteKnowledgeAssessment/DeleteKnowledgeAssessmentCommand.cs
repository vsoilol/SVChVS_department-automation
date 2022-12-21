using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.KnowledgeControlForms.Commands.DeleteKnowledgeAssessment
{
    public class DeleteKnowledgeAssessmentCommand : IRequest
    {
        public int WeekId { get; set; }

        public int KnowledgeControlFormId { get; set; }
    }

    public class DeleteKnowledgeAssessmentCommandHandler : IRequestHandler<DeleteKnowledgeAssessmentCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteKnowledgeAssessmentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteKnowledgeAssessmentCommand request, CancellationToken cancellationToken)
        {
            var knowledgeControlForm = await _context.KnowledgeControlForms
                .Include(_ => _.KnowledgeAssessments)
                .FirstOrDefaultAsync(_ => _.Id == request.KnowledgeControlFormId, cancellationToken: cancellationToken);

            knowledgeControlForm.KnowledgeAssessments
                .RemoveAll(_ => _.WeekId == request.WeekId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
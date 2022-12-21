using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.MethodicalRecommendations.Commands.DeleteMethodicalRecommendation
{
    public class DeleteMethodicalRecommendationCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteMethodicalRecommendationCommandHandler : IRequestHandler<DeleteMethodicalRecommendationCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteMethodicalRecommendationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMethodicalRecommendationCommand request, CancellationToken cancellationToken)
        {
            var methodicalRecommendation = await _context.MethodicalRecommendations
                .FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

            _context.MethodicalRecommendations.Remove(methodicalRecommendation);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
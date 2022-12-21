using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.MethodicalRecommendations.Commands.UpdateMethodicalRecommendation
{
    public class UpdateMethodicalRecommendationCommand : IRequest
    {
        public int Id { get; set; }

        public string Link { get; set; }

        public string Content { get; set; }
    }

    public class UpdateMethodicalRecommendationCommandHandler : IRequestHandler<UpdateMethodicalRecommendationCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMethodicalRecommendationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMethodicalRecommendationCommand request, CancellationToken cancellationToken)
        {
            var methodicalRecommendation = await _context.MethodicalRecommendations
                .FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

            methodicalRecommendation.Link = request.Link;
            methodicalRecommendation.Content = request.Content;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
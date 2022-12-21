using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.MethodicalRecommendations.Commands.CreateMethodicalRecommendation
{
    public class CreateMethodicalRecommendationCommand : IRequest<int>
    {
        public int EducationalProgramId { get; set; }

        public string Link { get; set; }

        public string Content { get; set; }
    }

    public class CreateMethodicalRecommendationCommandHandler : IRequestHandler<CreateMethodicalRecommendationCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateMethodicalRecommendationCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMethodicalRecommendationCommand request, CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.MethodicalRecommendations)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);

            var newMethodicalRecommendation = new MethodicalRecommendation
            {
                Link = request.Link,
                Content = request.Content
            };

            educationalProgram.MethodicalRecommendations.Add(newMethodicalRecommendation);
            await _context.SaveChangesAsync(cancellationToken);

            return newMethodicalRecommendation.Id;
        }
    }
}
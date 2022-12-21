using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Reviewers.Commands.UpdateReviewer
{
    public class UpdateReviewerCommand : IRequest
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }
    }

    public class UpdateReviewerCommandHandler : IRequestHandler<UpdateReviewerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateReviewerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateReviewerCommand request, CancellationToken cancellationToken)
        {
            var reviewer = await _context.Reviewers
                .FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

            reviewer.Name = request.Name;
            reviewer.Surname = request.Surname;
            reviewer.Patronymic = request.Patronymic;
            reviewer.Position = request.Position;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
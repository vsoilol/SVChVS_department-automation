using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Literatures.Commands.UpdateLiterature
{
    public class UpdateLiteratureCommand : IRequest
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Recommended { get; set; }

        public string SetNumber { get; set; }
    }

    public class UpdateLiteratureCommandHandler : IRequestHandler<UpdateLiteratureCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateLiteratureCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLiteratureCommand request, CancellationToken cancellationToken)
        {
            var literature = await _context.Literatures
                .FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

            literature.Description = request.Description;
            literature.Recommended = request.Recommended;
            literature.SetNumber = request.SetNumber;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
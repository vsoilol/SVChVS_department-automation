using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Literatures.Commands.DeleteLiterature
{
    public class DeleteLiteratureCommand : IRequest
    {
        public int LiteratureId { get; set; }
    }

    public class DeleteLiteratureCommandHandler : IRequestHandler<DeleteLiteratureCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteLiteratureCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteLiteratureCommand request, CancellationToken cancellationToken)
        {
            var literature = await _context.Literatures
                .FirstOrDefaultAsync(_ => _.Id == request.LiteratureId, cancellationToken: cancellationToken);

            _context.Literatures.Remove(literature);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
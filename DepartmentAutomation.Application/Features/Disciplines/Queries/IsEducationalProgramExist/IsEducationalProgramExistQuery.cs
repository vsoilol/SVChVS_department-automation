using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Disciplines.Queries.IsEducationalProgramExist
{
    public class IsEducationalProgramExistQuery : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class IsEducationalProgramExistQueryHandler : IRequestHandler<IsEducationalProgramExistQuery, bool>
    {
        private readonly IApplicationDbContext _context;

        public IsEducationalProgramExistQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(IsEducationalProgramExistQuery request, CancellationToken cancellationToken)
        {
            var isExist = await _context.EducationalPrograms
                .AnyAsync(_ => _.DisciplineId == request.Id, cancellationToken: cancellationToken);

            return isExist;
        }
    }
}
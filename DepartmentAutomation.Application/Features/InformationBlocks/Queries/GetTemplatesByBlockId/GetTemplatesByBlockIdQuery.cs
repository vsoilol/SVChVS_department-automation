using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetTemplatesByBlockId
{
    public class GetTemplatesByBlockIdQuery : IRequest<List<string>>
    {
        public int InformationBlockId { get; set; }
    }

    public class GetTemplatesByBlockIdQueryHandler : IRequestHandler<GetTemplatesByBlockIdQuery, List<string>>
    {
        private readonly IApplicationDbContext _context;

        public GetTemplatesByBlockIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(GetTemplatesByBlockIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = _context.InformationTemplates
                .Where(_ => _.InformationBlockId == request.InformationBlockId)
                .Select(_ => _.Content);

            return await result.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
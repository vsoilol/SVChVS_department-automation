using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetEditableBlocksByProgramId
{
    public class GetEditableBlocksByProgramIdQuery : IRequest<List<InformationBlockDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetEditableBlocksByProgramIdQueryHandler : IRequestHandler<GetEditableBlocksByProgramIdQuery, List<InformationBlockDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEditableBlocksByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InformationBlockDto>> Handle(GetEditableBlocksByProgramIdQuery request, CancellationToken cancellationToken)
        {
           var data = _context.InformationBlockContents
                .Where(_ => _.EducationalProgramId == request.EducationalProgramId &&
                            _.InformationBlock.Number.StartsWith("5"));

            return await data.ProjectTo<InformationBlockDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
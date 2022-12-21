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

namespace DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetMainBlocksByProgramId
{
    public class GetMainBlocksByProgramIdQuery : IRequest<List<InformationBlockDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class
        GetMainBlocksByProgramIdQueryHandler : IRequestHandler<GetMainBlocksByProgramIdQuery, List<InformationBlockDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMainBlocksByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InformationBlockDto>> Handle(GetMainBlocksByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.InformationBlockContents.Where(_ =>
                _.EducationalProgramId == request.EducationalProgramId && 
                (_.InformationBlock.Number.StartsWith("1") || 
                _.InformationBlock.Number.StartsWith("2.3")));

            var info = await data.ProjectTo<InformationBlockDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);

            return info;
        }
    }
}
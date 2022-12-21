using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Contracts.Responses;

namespace DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetNotChoosenBlocksByProgramId
{
    public class GetNotChoosenBlocksByProgramIdQuery : IRequest<List<InformationBlockWithoutContentDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class
        GetNotChoosenBlocksByProgramIdQueryHandler : IRequestHandler<GetNotChoosenBlocksByProgramIdQuery,
            List<InformationBlockWithoutContentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetNotChoosenBlocksByProgramIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<InformationBlockWithoutContentDto>> Handle(GetNotChoosenBlocksByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = _context.InformationBlocks
                .Where(_ => _.InformationBlockContents.All(content =>
                    content.EducationalProgramId != request.EducationalProgramId));

            return await result.ProjectTo<InformationBlockWithoutContentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
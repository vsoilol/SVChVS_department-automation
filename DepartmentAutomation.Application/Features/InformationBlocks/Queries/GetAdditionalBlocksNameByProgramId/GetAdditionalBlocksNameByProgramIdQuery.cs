using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetAdditionalBlocksNameByProgramId
{
    public class GetAdditionalBlocksNameByProgramIdQuery : IRequest<List<InformationBlockName>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetAdditionalBlocksNameByProgramIdQueryHandler : IRequestHandler<GetAdditionalBlocksNameByProgramIdQuery,
            List<InformationBlockName>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAdditionalBlocksNameByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<InformationBlockName>> Handle(GetAdditionalBlocksNameByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.InformationBlocks
                .Where(_ =>
                    _.InformationBlockContents
                        .Any(block => block.EducationalProgramId == request.EducationalProgramId)
                    && _.Number.StartsWith("5"));
            
            return await data.ProjectTo<InformationBlockName>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
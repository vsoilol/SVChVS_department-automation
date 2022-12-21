using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Indicators.Queries.GetIndicatorWithLevelsByProgramId
{
    public class GetIndicatorWithLevelsByProgramIdQuery : IRequest<List<IndicatorWithLevelsDto>>
    {
        public int EducationalProgramId { get; set; }
    }
    
    public class GetIndicatorWithLevelsByProgramIdQueryHandler: 
        IRequestHandler<GetIndicatorWithLevelsByProgramIdQuery, List<IndicatorWithLevelsDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetIndicatorWithLevelsByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<IndicatorWithLevelsDto>> Handle(GetIndicatorWithLevelsByProgramIdQuery request, CancellationToken cancellationToken)
        {
            var data = _context.Indicators
                .Where(_ => _.CompetenceFormationLevels
                    .Any(level => level.EducationalProgramId == request.EducationalProgramId))
                .OrderBy(_ => _.Competence.Code);

            return await data
                .ProjectTo<IndicatorWithLevelsDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
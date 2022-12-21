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

namespace DepartmentAutomation.Application.Features.CompetenceFormationLevels.Queries.
    GetCompetenceFormationLevelByCompetence
{
    public class GetCompetenceFormationLevelByCompetenceQuery : IRequest<List<CompetenceFormationLevelDto>>
    {
        public int EducationalProgramId { get; set; }

        public int CompetenceId { get; set; }
    }

    public class GetCompetenceFormationLevelByCompetenceQueryHandler : IRequestHandler<
        GetCompetenceFormationLevelByCompetenceQuery, List<CompetenceFormationLevelDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompetenceFormationLevelByCompetenceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CompetenceFormationLevelDto>> Handle(
            GetCompetenceFormationLevelByCompetenceQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.CompetenceFormationLevels
                .Include(_ => _.Indicator)
                .Where(_ => _.EducationalProgramId == request.EducationalProgramId
                            && _.Indicator.CompetenceId == request.CompetenceId);

            return await data
                .ProjectTo<CompetenceFormationLevelDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
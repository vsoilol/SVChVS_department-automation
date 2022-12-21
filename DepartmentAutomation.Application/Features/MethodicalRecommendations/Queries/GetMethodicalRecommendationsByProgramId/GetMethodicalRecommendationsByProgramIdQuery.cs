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

namespace DepartmentAutomation.Application.Features.MethodicalRecommendations.Queries.
    GetMethodicalRecommendationsByProgramId
{
    public class GetMethodicalRecommendationsByProgramIdQuery : IRequest<List<MethodicalRecommendationDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetMethodicalRecommendationsByProgramIdQueryHandler : IRequestHandler<
        GetMethodicalRecommendationsByProgramIdQuery, List<MethodicalRecommendationDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMethodicalRecommendationsByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MethodicalRecommendationDto>> Handle(GetMethodicalRecommendationsByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.MethodicalRecommendations
                .Where(_ => _.EducationalPrograms
                    .Any(program => program.Id == request.EducationalProgramId));

            return await data
                .ProjectTo<MethodicalRecommendationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
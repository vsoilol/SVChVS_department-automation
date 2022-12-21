using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;

namespace DepartmentAutomation.Application.Features.Audiences.Queries.GetAllAudiencesByProgramId
{
    public class GetAllAudiencesByProgramIdQuery : IRequest<List<AudienceBriefInfoDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetAllAudiencesByProgramIdQueryHandler : IRequestHandler<GetAllAudiencesByProgramIdQuery,
        List<AudienceBriefInfoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllAudiencesByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AudienceBriefInfoDto>> Handle(GetAllAudiencesByProgramIdQuery request, CancellationToken cancellationToken)
        {
            var audiences = _context.Audiences
                .Where(_ => _.EducationalPrograms
                    .Any(program => program.Id == request.EducationalProgramId));

            return await audiences
                .ProjectTo<AudienceBriefInfoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;

namespace DepartmentAutomation.Application.Features.Audiences.Queries.GetAllAudience
{
    public class GetAllAudienceQuery : IRequest<List<AudienceBriefInfoDto>>
    {
    }

    public class GetAllAudienceQueryHandler : IRequestHandler<GetAllAudienceQuery, List<AudienceBriefInfoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllAudienceQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AudienceBriefInfoDto>> Handle(GetAllAudienceQuery request, CancellationToken cancellationToken)
        {
            var audiences = _context.Audiences;

            return await audiences
                .ProjectTo<AudienceBriefInfoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Literatures.Queries.GetLiteraturesByType
{
    public class GetLiteraturesByTypeQuery : IRequest<List<LiteratureDto>>
    {
        public int EducationalProgramId { get; set; }

        public LiteratureType LiteratureType { get; set; }
    }

    public class GetLiteraturesByTypeQueryHandler : IRequestHandler<GetLiteraturesByTypeQuery, List<LiteratureDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLiteraturesByTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LiteratureDto>> Handle(GetLiteraturesByTypeQuery request,
            CancellationToken cancellationToken)
        {
            var literatures = _context.LiteratureTypeInfos
                .Where(_ => _.EducationalProgramId == request.EducationalProgramId
                            && _.Type == request.LiteratureType)
                .Select(_ => _.Literature);

            return await literatures
                .ProjectTo<LiteratureDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
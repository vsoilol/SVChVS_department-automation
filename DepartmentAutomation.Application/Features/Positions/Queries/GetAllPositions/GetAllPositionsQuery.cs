using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Positions.Queries.GetAllPositions
{
    public class GetAllPositionsQuery : IRequest<List<PositionDto>>
    {
    }

    public class GetAllDepartmentsQueryHandle : IRequestHandler<GetAllPositionsQuery, List<PositionDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandle(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PositionDto>> Handle(GetAllPositionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Positions
                .ProjectTo<PositionDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
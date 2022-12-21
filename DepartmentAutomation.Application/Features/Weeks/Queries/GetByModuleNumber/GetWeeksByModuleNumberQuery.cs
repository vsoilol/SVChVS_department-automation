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

namespace DepartmentAutomation.Application.Features.Weeks.Queries.GetByModuleNumber
{
    public class GetWeeksByModuleNumberQuery : IRequest<List<WeekDto>>
    {
        public int EducationalProgramId { get; set; }

        public int SemesterId { get; set; }

        public int ModuleNumber { get; set; }
    }

    public class GetWeeksByModuleNumberQueryHandler : IRequestHandler<GetWeeksByModuleNumberQuery, List<WeekDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWeeksByModuleNumberQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<WeekDto>> Handle(GetWeeksByModuleNumberQuery request, CancellationToken cancellationToken)
        {
            return await _context.Weeks.Where(_ =>
                _.EducationalProgramId == request.EducationalProgramId
                && _.SemesterId == request.SemesterId
                && _.TrainingModuleNumber == request.ModuleNumber)
                .OrderBy(_ => _.Number).ProjectTo<WeekDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
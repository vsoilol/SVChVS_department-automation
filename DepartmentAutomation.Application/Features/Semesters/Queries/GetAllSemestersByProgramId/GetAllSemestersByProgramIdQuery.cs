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

namespace DepartmentAutomation.Application.Features.Semesters.Queries.GetAllSemestersByProgramId
{
    public class GetAllSemestersByProgramIdQuery : IRequest<List<SemesterDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class
        GetAllSemestersByProgramIdQueryHandler : IRequestHandler<GetAllSemestersByProgramIdQuery, List<SemesterDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSemestersByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SemesterDto>> Handle(GetAllSemestersByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var disciplineId = (await _context.EducationalPrograms
                    .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId,
                        cancellationToken: cancellationToken))
                .DisciplineId;

            var semesters = _context.Semesters
                .Include(_ => _.Weeks)
                .Where(_ => _.SemesterDistributions
                    .Any(distribution => distribution.DisciplineId == disciplineId))
                .OrderBy(_ => _.Number);

            return await semesters
                .ProjectTo<SemesterDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
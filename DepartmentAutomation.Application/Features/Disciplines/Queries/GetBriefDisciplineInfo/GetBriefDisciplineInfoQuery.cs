using AutoMapper;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;

namespace DepartmentAutomation.Application.Features.Disciplines.Queries.GetBriefDisciplineInfo
{
    public class GetBriefDisciplineInfoQuery : IRequest<DisciplineBriefDto>
    {
        public int DisciplineId { get; set; }
    }

    public class GetBriefDisciplineInfoQueryHandler : IRequestHandler<GetBriefDisciplineInfoQuery, DisciplineBriefDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBriefDisciplineInfoQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DisciplineBriefDto> Handle(GetBriefDisciplineInfoQuery request, CancellationToken cancellationToken)
        {
            var discipline = await _context.Disciplines
                .FirstOrDefaultAsync(_ => _.Id == request.DisciplineId, cancellationToken: cancellationToken);

            return _mapper.Map<DisciplineBriefDto>(discipline);
        }
    }
}
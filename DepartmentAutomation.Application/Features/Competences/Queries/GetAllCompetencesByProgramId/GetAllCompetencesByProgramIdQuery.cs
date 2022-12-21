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

namespace DepartmentAutomation.Application.Features.Competences.Queries.GetAllCompetencesByProgramId
{
    public class GetAllCompetencesByProgramIdQuery : IRequest<List<CompetenceDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class
        GetAllCompetencesByProgramIdQueryHandler : IRequestHandler<GetAllCompetencesByProgramIdQuery,
            List<CompetenceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllCompetencesByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CompetenceDto>> Handle(GetAllCompetencesByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var competences =
                _context.Indicators.Where(_ =>
                        _.Disciplines.Any(discipline =>
                            discipline.EducationalProgram != null &&
                            discipline.EducationalProgram.Id == request.EducationalProgramId))
                    .Select(_ => _.Competence);

            return await competences
                .ProjectTo<CompetenceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
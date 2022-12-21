using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Curriculums.Queries.GetAllYearsByDepartmentId
{
    public class GetAllYearsByDepartmentIdQuery : IRequest<List<CurriculumsStudyStartingYearDto>>
    {
         public int DepartmentId { get; set; }
    }
    
    public class GetAllYearsByDepartmentIdQueryHandler : IRequestHandler<
        GetAllYearsByDepartmentIdQuery, List<CurriculumsStudyStartingYearDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllYearsByDepartmentIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<List<CurriculumsStudyStartingYearDto>> Handle(
            GetAllYearsByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            var data = _context.Disciplines
                .Where(_ => _.DepartmentId == request.DepartmentId)
                .Select(_ => _.Curriculum.StudyStartingYear.Year)
                .Distinct();

            return await data
                .ProjectTo<CurriculumsStudyStartingYearDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
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

namespace DepartmentAutomation.Application.Features.EvaluationTools.Queries.GetNotChoosenEvaluationTool
{
    public class GetNotChoosenEvaluationToolQuery : IRequest<List<EvaluationToolTypeDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetNotChoosenEvaluationToolQueryHandler : IRequestHandler<GetNotChoosenEvaluationToolQuery, List<EvaluationToolTypeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetNotChoosenEvaluationToolQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EvaluationToolTypeDto>> Handle(GetNotChoosenEvaluationToolQuery request, CancellationToken cancellationToken)
        {
            var result = _context.EvaluationToolTypes
                .Where(_ => _.EvaluationTools.All(content =>
                    content.EducationalProgramId != request.EducationalProgramId));

            return await result.ProjectTo<EvaluationToolTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
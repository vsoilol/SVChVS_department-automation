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

namespace DepartmentAutomation.Application.Features.EvaluationTools.Queries.GetEvaluationToolTypeByProgramId
{
    public class GetEvaluationToolTypeByProgramIdQuery : IRequest<List<EvaluationToolTypeDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetEvaluationToolTypeByProgramIdQueryHandler : IRequestHandler<GetEvaluationToolTypeByProgramIdQuery, List<EvaluationToolTypeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEvaluationToolTypeByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EvaluationToolTypeDto>> Handle(GetEvaluationToolTypeByProgramIdQuery request, CancellationToken cancellationToken)
        {
            var data = _context.EvaluationTools
                .Where(_ => _.EducationalProgramId == request.EducationalProgramId)
                .Select(_ => _.EvaluationToolType);

            return await data.ProjectTo<EvaluationToolTypeDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
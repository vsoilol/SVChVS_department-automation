using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;

namespace DepartmentAutomation.Application.Features.EvaluationTools.Queries.GetAllEvaluationToolByProgramId
{
    public class GetAllEvaluationToolByProgramIdQuery : IRequest<List<EvaluationToolBriefDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class
        GetAllEvaluationToolByProgramIdQueryHandler : IRequestHandler<GetAllEvaluationToolByProgramIdQuery,
            List<EvaluationToolBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllEvaluationToolByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EvaluationToolBriefDto>> Handle(GetAllEvaluationToolByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.EvaluationTools
                .Where(_ => _.EducationalProgramId == request.EducationalProgramId);

            return await data.ProjectTo<EvaluationToolBriefDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
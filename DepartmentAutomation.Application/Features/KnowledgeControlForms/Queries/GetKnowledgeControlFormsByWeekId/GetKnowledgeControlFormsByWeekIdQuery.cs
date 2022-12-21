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

namespace DepartmentAutomation.Application.Features.KnowledgeControlForms.Queries.GetKnowledgeControlFormsByWeekId
{
    public class GetKnowledgeControlFormsByWeekIdQuery : IRequest<List<KnowledgeAssessmentDto>>
    {
        public int WeekId { get; set; }
    }

    public class GetKnowledgeControlFormsByWeekIdQueryHandler : IRequestHandler<GetKnowledgeControlFormsByWeekIdQuery,
        List<KnowledgeAssessmentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetKnowledgeControlFormsByWeekIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<KnowledgeAssessmentDto>> Handle(GetKnowledgeControlFormsByWeekIdQuery request,
            CancellationToken cancellationToken)
        {
            var knowledgeControlForms = _context.KnowledgeControlForms
                .Where(_ => _.KnowledgeAssessments.Count > 0)
                .Select(_ =>
                    _.KnowledgeAssessments.FirstOrDefault(assessment =>
                        assessment.WeekId == request.WeekId));

            return await knowledgeControlForms
                .ProjectTo<KnowledgeAssessmentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
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

namespace DepartmentAutomation.Application.Features.KnowledgeControlForms.Queries.GetAllKnowledgeControlForm
{
    public class GetAllKnowledgeControlFormQuery : IRequest<List<KnowledgeControlFormDto>>
    {
    }

    public class GetAllKnowledgeControlFormQueryHandler : IRequestHandler<GetAllKnowledgeControlFormQuery, List<KnowledgeControlFormDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllKnowledgeControlFormQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<KnowledgeControlFormDto>> Handle(GetAllKnowledgeControlFormQuery request, CancellationToken cancellationToken)
        {
            return await _context.KnowledgeControlForms
                .Where(x => !x.ShortName.Contains("ПА"))
                .ProjectTo<KnowledgeControlFormDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
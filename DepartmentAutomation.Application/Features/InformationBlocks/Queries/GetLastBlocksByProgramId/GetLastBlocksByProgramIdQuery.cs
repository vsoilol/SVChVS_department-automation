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

namespace DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetLastBlocksByProgramId
{
    public class GetLastBlocksByProgramIdQuery : IRequest<List<InformationBlockDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetLastBlocksByProgramIdQueryHandler : IRequestHandler<GetLastBlocksByProgramIdQuery, List<InformationBlockDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLastBlocksByProgramIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<InformationBlockDto>> Handle(GetLastBlocksByProgramIdQuery request, CancellationToken cancellationToken)
        {
            var data = _context.InformationBlocks.Where(_ =>
                    _.Number.StartsWith("7"))
                .Select(_ =>
                    _.InformationBlockContents.FirstOrDefault(content =>
                        content.EducationalProgramId == request.EducationalProgramId));

            return await data.ProjectTo<InformationBlockDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
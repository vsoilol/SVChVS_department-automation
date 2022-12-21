using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetBlockByNumber
{
    public class GetBlockByNumberQuery: IRequest<InformationBlockDto>
    {
        public int EducationalProgramId { get; set; }
        
        public string Number { get; set; }
    }
    
    public class GetBlockByNumberQueryHandler: IRequestHandler<GetBlockByNumberQuery, InformationBlockDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBlockByNumberQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<InformationBlockDto> Handle(GetBlockByNumberQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.InformationBlockContents
                .Include(_ => _.InformationBlock)
                .FirstOrDefaultAsync(_ =>
                _.EducationalProgramId == request.EducationalProgramId && 
                _.InformationBlock.Number.StartsWith(request.Number), cancellationToken: cancellationToken);

            return _mapper.Map<InformationBlockDto>(data);
        }
    }
}
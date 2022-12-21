using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Disciplines.Queries.GetAdditionalDisciplineInfoById
{
    public class GetAdditionalDisciplineInfoByIdQuery : IRequest<AdditionalDisciplineInfoDto>
    {
        public int DisciplineId { get; set; }
    }
    
    public class GetAdditionalDisciplineInfoByIdQueryHandler : IRequestHandler<
        GetAdditionalDisciplineInfoByIdQuery, AdditionalDisciplineInfoDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAdditionalDisciplineInfoByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<AdditionalDisciplineInfoDto> Handle(
            GetAdditionalDisciplineInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var data = _context.Disciplines.Where(_ => _.Id == request.DisciplineId);
            
            return await data
                .ProjectTo<AdditionalDisciplineInfoDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
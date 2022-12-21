using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersByDepartmentId
{
    public class GetTeachersByDepartmentIdQuery : IRequest<List<TeacherFullNameDto>>
    {
        public int DepartmentId { get; set; }
    }
    
    public class GetTeachersByDepartmentIdQueryHandler : IRequestHandler<
        GetTeachersByDepartmentIdQuery, List<TeacherFullNameDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTeachersByDepartmentIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<TeacherFullNameDto>> Handle(GetTeachersByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            var data = _context.Teachers
                .Where(_ => _.DepartmentId == request.DepartmentId);

            return await data
                .ProjectTo<TeacherFullNameDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
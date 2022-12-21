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

namespace DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersFullNameByDisciplineId
{
    public class GetTeachersFullNameByDisciplineIdQuery : IRequest<List<TeacherFullNameDto>>
    {
        public int DisciplineId { get; set; }
    }
    
    public class GetTeachersFullNameByDisciplineIdQueryHandler : IRequestHandler<
        GetTeachersFullNameByDisciplineIdQuery, List<TeacherFullNameDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTeachersFullNameByDisciplineIdQueryHandler(
            IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<TeacherFullNameDto>> Handle(
            GetTeachersFullNameByDisciplineIdQuery request, CancellationToken cancellationToken)
        {
            var data = _context.Teachers
                .Where(_ => _.Disciplines
                    .Any(discipline => discipline.Id == request.DisciplineId));

            return await data
                .ProjectTo<TeacherFullNameDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
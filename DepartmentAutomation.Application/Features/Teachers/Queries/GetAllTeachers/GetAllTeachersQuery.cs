using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Teachers.Queries.GetAllTeachers
{
    public class GetAllTeachersQuery : IRequest<List<TeacherBriefDto>>
    {
    }

    public class GetAllTeachersQueryHandle : IRequestHandler<GetAllTeachersQuery, List<TeacherBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTeachersQueryHandle(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TeacherBriefDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = _context.Teachers.ProjectTo<TeacherBriefDto>(_mapper.ConfigurationProvider);
            return await teachers.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
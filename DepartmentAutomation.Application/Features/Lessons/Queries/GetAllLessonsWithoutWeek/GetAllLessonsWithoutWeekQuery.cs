using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLessonsWithoutWeek
{
    public class GetAllLessonsWithoutWeekQuery : IRequest<List<LessonDto>>
    {
        public int EducationalProgramId { get; set; }

        public LessonType LessonType { get; set; }
    }

    public class GetAllLessonsWithoutWeekQueryHandler : IRequestHandler<GetAllLessonsWithoutWeekQuery, List<LessonDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllLessonsWithoutWeekQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LessonDto>> Handle(GetAllLessonsWithoutWeekQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.Lessons
                .Include(_ => _.Weeks)
                .Where(_ => _.EducationalProgramId == request.EducationalProgramId
                            && _.LessonType == request.LessonType
                            && _.Weeks.Count <= 0);

            return await data
                .ProjectTo<LessonDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
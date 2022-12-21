using AutoMapper;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Lessons.Queries.GetLessonByWeekId
{
    public class GetLessonByWeekIdQuery : IRequest<LessonDto>
    {
        public int WeekId { get; set; }

        public LessonType LessonType { get; set; }
    }

    public class GetLessonByWeekIdQueryHandler : IRequestHandler<GetLessonByWeekIdQuery, LessonDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLessonByWeekIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LessonDto> Handle(GetLessonByWeekIdQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(_ => _.Weeks
                        .Any(week => week.Id == request.WeekId) && _.LessonType == request.LessonType,
                    cancellationToken: cancellationToken);

            return _mapper.Map<LessonDto>(lesson);
        }
    }
}
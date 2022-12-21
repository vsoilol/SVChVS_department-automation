using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLecturesByProgramId
{
    public class GetAllLecturesByProgramIdQuery : IRequest<List<LecturesBriefDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class
        GetAllLecturesByProgramIdQueryHandler : IRequestHandler<GetAllLecturesByProgramIdQuery, List<LecturesBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllLecturesByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<LecturesBriefDto>> Handle(GetAllLecturesByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.Lessons.Where(_ =>
                    _.EducationalProgramId == request.EducationalProgramId && 
                    _.LessonType == LessonType.Lecture)
                .OrderBy(_ => _.Number);

            return await data
                .ProjectTo<LecturesBriefDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
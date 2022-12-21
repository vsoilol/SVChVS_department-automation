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

namespace DepartmentAutomation.Application.Features.Lessons.Queries.GetAllLessonsByProgramId
{
    public class GetAllLessonsByProgramIdQuery : IRequest<List<LessonDto>>
    {
        public int EducationalProgramId { get; set; }

        public LessonType LessonType { get; set; }
    }

    public class
        GetAllLessonsByProgramIdQueryHandler : IRequestHandler<GetAllLessonsByProgramIdQuery, List<LessonDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllLessonsByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LessonDto>> Handle(GetAllLessonsByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.Lessons.Where(_ =>
                _.EducationalProgramId == request.EducationalProgramId && _.LessonType == request.LessonType)
                .OrderBy(_ => _.Number);

            return await data
                .ProjectTo<LessonDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
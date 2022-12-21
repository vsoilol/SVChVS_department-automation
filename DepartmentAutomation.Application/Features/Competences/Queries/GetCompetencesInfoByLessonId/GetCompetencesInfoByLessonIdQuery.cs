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

namespace DepartmentAutomation.Application.Features.Competences.Queries.GetCompetencesInfoByLessonId
{
    public class GetByLessonIdQuery : IRequest<List<CompetenceDto>>
    {
        public int LessonId { get; set; }
    }

    public class
        GetByLessonIdQueryHandler : IRequestHandler<GetByLessonIdQuery,
            List<CompetenceDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetByLessonIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CompetenceDto>> Handle(GetByLessonIdQuery request,
            CancellationToken cancellationToken)
        {
            var data = _context.Competences
                .Where(_ => _.Lessons.Any(lesson => lesson.Id == request.LessonId));

            return await data
                .ProjectTo<CompetenceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
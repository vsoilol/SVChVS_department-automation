using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Contracts.Responses;

namespace DepartmentAutomation.Application.Features.TrainingCourseForms.Queries.GetAllTrainingCourseFormWithoutLessons
{
    public class GetAllTrainingCourseFormWithoutLessonsQuery : IRequest<List<TrainingCourseFormDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class
        GetAllTrainingCourseFormWithoutLessonsQueryHandler : IRequestHandler<GetAllTrainingCourseFormWithoutLessonsQuery
            , List<TrainingCourseFormDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTrainingCourseFormWithoutLessonsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TrainingCourseFormDto>> Handle(GetAllTrainingCourseFormWithoutLessonsQuery request,
            CancellationToken cancellationToken)
        {
            var lessons = _context.Lessons.Where(_ => _.EducationalProgramId == request.EducationalProgramId);

            var data = _context.TrainingCourseForms
                .Include(_ => _.Lessons)
                .Where(_ => _.Lessons.All(lesson => !lessons.Contains(lesson)));

            return await data
                .ProjectTo<TrainingCourseFormDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
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

namespace DepartmentAutomation.Application.Features.TrainingCourseForms.Queries.GetAllTrainingCourseFormByProgramId
{
    public class GetAllTrainingCourseFormByProgramIdQuery : IRequest<List<TrainingCourseFormDto>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetAllTrainingCourseFormByProgramIdQueryHandler : IRequestHandler<
        GetAllTrainingCourseFormByProgramIdQuery, List<TrainingCourseFormDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTrainingCourseFormByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TrainingCourseFormDto>> Handle(GetAllTrainingCourseFormByProgramIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.TrainingCourseForms.Where(_ =>
                    _.EducationalPrograms.Any(program => program.Id == request.EducationalProgramId))
                .ProjectTo<TrainingCourseFormDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
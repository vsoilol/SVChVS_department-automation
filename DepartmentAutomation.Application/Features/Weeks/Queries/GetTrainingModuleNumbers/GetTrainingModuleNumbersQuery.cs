using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Weeks.Queries.GetTrainingModuleNumbers
{
    public class GetTrainingModuleNumbersQuery : IRequest<List<int>>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetTrainingModuleNumbersQueryHandler : IRequestHandler<GetTrainingModuleNumbersQuery, List<int>>
    {
        private readonly IApplicationDbContext _context;

        public GetTrainingModuleNumbersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> Handle(GetTrainingModuleNumbersQuery request, CancellationToken cancellationToken)
        {
            var trainingModuleNumbers = await _context.Weeks
                .Where(_ => _.EducationalProgramId == request.EducationalProgramId)
                .Select(_ => _.TrainingModuleNumber).ToListAsync(cancellationToken: cancellationToken);

            return trainingModuleNumbers.Distinct().ToList();
        }
    }
}
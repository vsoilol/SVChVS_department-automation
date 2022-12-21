using AutoMapper;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.Reviewers.Queries.GetReviewerByProgramId
{
    public class GetReviewerByProgramIdQuery : IRequest<ReviewerDto>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetReviewerByProgramIdQueryHandler : IRequestHandler<GetReviewerByProgramIdQuery, ReviewerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReviewerByProgramIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReviewerDto> Handle(GetReviewerByProgramIdQuery request, CancellationToken cancellationToken)
        {
            var reviewer = await _context.Reviewers
                .FirstOrDefaultAsync(_ => _.EducationalProgram.Id == request.EducationalProgramId,
                    cancellationToken: cancellationToken);

            return _mapper.Map<ReviewerDto>(reviewer);
        }
    }
}
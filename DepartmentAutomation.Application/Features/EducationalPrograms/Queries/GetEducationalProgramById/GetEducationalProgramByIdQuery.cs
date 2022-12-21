using AutoMapper;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetEducationalProgramById
{
    public class GetEducationalProgramByIdQuery : IRequest<EducationalProgramDto>
    {
        public int Id { get; set; }
    }

    public class
        GetEducationalProgramByIdQueryHandle : IRequestHandler<GetEducationalProgramByIdQuery, EducationalProgramDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEducationalProgramByIdQueryHandle(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EducationalProgramDto> Handle(GetEducationalProgramByIdQuery request,
            CancellationToken cancellationToken)
        {
            var educationalProgram =
                await _context.EducationalPrograms
                    .Include(_ => _.Discipline)
                    .FirstOrDefaultAsync(
                        _ => _.Id == request.Id,
                        cancellationToken: cancellationToken);

            return _mapper.Map<EducationalProgramDto>(educationalProgram);
        }
    }
}
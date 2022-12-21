using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Contracts.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Lessons.Queries.GetLectureById
{
    public class GetLectureByIdQuery : IRequest<LectureDto>
    {
        public int Id { get; set; }
    }
    
    public class GetLectureByIdQueryHandler : IRequestHandler<GetLectureByIdQuery, LectureDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLectureByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<LectureDto> Handle(GetLectureByIdQuery request, CancellationToken cancellationToken)
        {
            var lecture = await _context.Lessons
                .Include(_ => _.Competences)
                .FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);
            
            return _mapper.Map<LectureDto>(lecture);
        }
    }
}
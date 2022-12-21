using AutoMapper;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Models.WordDocument;

namespace DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetProgramWordDocument
{
    public class GetProgramWordDocumentQuery : IRequest<byte[]>
    {
        public int EducationalProgramId { get; set; }
    }

    public class GetProgramWordDocumentQueryHandler : IRequestHandler<GetProgramWordDocumentQuery, byte[]>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IWordDocumentService _wordDocumentService;

        public GetProgramWordDocumentQueryHandler(IMapper mapper, IApplicationDbContext context,
            IWordDocumentService wordDocumentService)
        {
            _mapper = mapper;
            _context = context;
            _wordDocumentService = wordDocumentService;
        }

        public async Task<byte[]> Handle(GetProgramWordDocumentQuery request, CancellationToken cancellationToken)
        {
            var educationalProgram = await _context.EducationalPrograms
                .Include(_ => _.Reviewer)
                .FirstOrDefaultAsync(_ => _.Id == request.EducationalProgramId, cancellationToken: cancellationToken);


            var educationalProgramWord = _mapper.Map<EducationalProgram>(educationalProgram);
            var content = _wordDocumentService.GenerateDocument(educationalProgramWord);

            return content;
        }
    }
}
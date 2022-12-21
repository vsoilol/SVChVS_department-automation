using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Domain.Entities.LiteratureInfo;

namespace DepartmentAutomation.Application.Features.Literatures.Commands.CreateLiterature
{
    public class CreateLiteratureCommand : IRequest<int>
    {
        public string Description { get; set; }

        public string Recommended { get; set; }

        public string SetNumber { get; set; }

        public LiteratureType LiteratureType { get; set; }

        public int EducationalProgramId { get; set; }
    }

    public class CreateLiteratureCommandHandler : IRequestHandler<CreateLiteratureCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateLiteratureCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLiteratureCommand request, CancellationToken cancellationToken)
        {
            var newLiterature = new Literature
            {
                Description = request.Description,
                Recommended = request.Recommended,
                SetNumber = request.SetNumber,
            };

            var newLiteratureTypeInfo = new LiteratureTypeInfo
            {
                EducationalProgramId = request.EducationalProgramId,
                Literature = newLiterature,
                Type = request.LiteratureType
            };

            _context.LiteratureTypeInfos.Add(newLiteratureTypeInfo);
            await _context.SaveChangesAsync(cancellationToken);

            return newLiterature.Id;
        }
    }
}
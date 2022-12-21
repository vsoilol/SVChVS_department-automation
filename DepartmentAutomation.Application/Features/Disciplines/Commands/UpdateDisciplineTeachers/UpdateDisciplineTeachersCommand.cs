using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DepartmentAutomation.Application.Features.Disciplines.Commands.UpdateDisciplineTeachers
{
    public class UpdateDisciplineTeachersCommand : IRequest
    {
        public int DisciplineId { get; set; }
        
        public List<int> TeachersId { get; set; }
    }
    
    public class UpdateDisciplineTeachersCommandHandler : IRequestHandler<UpdateDisciplineTeachersCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateDisciplineTeachersCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Unit> Handle(UpdateDisciplineTeachersCommand request, CancellationToken cancellationToken)
        {
            var discipline = await _context.Disciplines
                .Include(_ => _.Teachers)
                .FirstOrDefaultAsync(_ => _.Id == request.DisciplineId, cancellationToken: cancellationToken);
            
            var teachers = await _context.Teachers
                .Where(_ => request.TeachersId.Contains(_.Id))
                .ToListAsync(cancellationToken: cancellationToken);

            discipline.Teachers = teachers;
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
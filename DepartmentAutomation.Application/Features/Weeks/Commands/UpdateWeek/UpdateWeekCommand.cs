using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DepartmentAutomation.Domain.Entities.KnowledgeControlFormInfo;
using DepartmentAutomation.Application.Contracts.Responses;

namespace DepartmentAutomation.Application.Features.Weeks.Commands.UpdateWeek
{
    public class UpdateWeekCommand : IRequest
    {
        public int Id { get; set; }

        public int IndependentWorkHours { get; set; }

        public List<KnowledgeAssessmentDto> KnowledgeAssessments { get; set; }

        public List<LessonDto> Lessons { get; set; }
    }

    public class UpdateWeekCommandHandler : IRequestHandler<UpdateWeekCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateWeekCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateWeekCommand request, CancellationToken cancellationToken)
        {
            var lessonsId = request.Lessons.Select(_ => _.Id);

            var lessons = _context.Lessons
                .Where(_ => lessonsId.Contains(_.Id));

            var week = await _context.Weeks
                .Include(_ => _.KnowledgeAssessments)
                .Include(_ => _.Lessons)
                .FirstOrDefaultAsync(_ => _.Id == request.Id,
                    cancellationToken: cancellationToken);

            week.IndependentWorkHours = request.IndependentWorkHours;

            week.KnowledgeAssessments = request.KnowledgeAssessments.Select(_ => new KnowledgeAssessment
            {
                KnowledgeControlFormId = _.Id,
                MaxMark = _.MaxMark,
            }).ToList();

            week.Lessons = await lessons.ToListAsync(cancellationToken: cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
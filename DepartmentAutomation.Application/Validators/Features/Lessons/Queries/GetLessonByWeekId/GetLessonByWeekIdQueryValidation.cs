using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetLessonByWeekId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Lessons.Queries.GetLessonByWeekId
{
    public class GetLessonByWeekIdQueryValidation : AbstractValidator<GetLessonByWeekIdQuery>
    {
        public GetLessonByWeekIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.WeekId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Week>(context));
        }
    }
}
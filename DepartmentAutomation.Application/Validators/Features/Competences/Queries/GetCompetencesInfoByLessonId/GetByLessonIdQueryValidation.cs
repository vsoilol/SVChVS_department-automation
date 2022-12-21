using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Competences.Queries.GetCompetencesInfoByLessonId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Competences.Queries.GetCompetencesInfoByLessonId
{
    public class GetByLessonIdQueryValidation : AbstractValidator<GetByLessonIdQuery>
    {
        public GetByLessonIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.LessonId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Lesson>(context));
        }
    }
}
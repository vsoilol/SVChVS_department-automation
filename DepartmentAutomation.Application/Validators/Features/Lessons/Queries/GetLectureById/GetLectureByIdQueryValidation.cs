using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Lessons.Queries.GetLectureById;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Lessons.Queries.GetLectureById
{
    public class GetLectureByIdQueryValidation : AbstractValidator<GetLectureByIdQuery>
    {
        public GetLectureByIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Lesson>(context));
        }
    }
}
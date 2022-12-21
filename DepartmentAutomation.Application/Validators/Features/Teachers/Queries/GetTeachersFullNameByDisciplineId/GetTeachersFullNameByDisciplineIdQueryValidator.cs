using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Teachers.Queries.GetTeachersFullNameByDisciplineId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Teachers.Queries.GetTeachersFullNameByDisciplineId
{
    public class GetTeachersFullNameByDisciplineIdQueryValidator : AbstractValidator<GetTeachersFullNameByDisciplineIdQuery>
    {
        public GetTeachersFullNameByDisciplineIdQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.DisciplineId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Discipline>(context));
        }
    }
}
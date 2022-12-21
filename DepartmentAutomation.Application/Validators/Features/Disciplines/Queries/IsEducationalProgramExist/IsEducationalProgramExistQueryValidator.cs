using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Disciplines.Queries.IsEducationalProgramExist;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Disciplines.Queries.IsEducationalProgramExist
{
    public class IsEducationalProgramExistQueryValidator : AbstractValidator<IsEducationalProgramExistQuery>
    {
        public IsEducationalProgramExistQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Discipline>(context));
        }
    }
}
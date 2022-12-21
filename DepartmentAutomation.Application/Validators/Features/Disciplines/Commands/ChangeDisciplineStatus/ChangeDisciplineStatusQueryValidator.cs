using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Disciplines.Commands.ChangeDisciplineStatus;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Disciplines.Commands.ChangeDisciplineStatus
{
    public class ChangeDisciplineStatusQueryValidator : AbstractValidator<ChangeDisciplineStatusCommand>
    {
        public ChangeDisciplineStatusQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Discipline>(context));
        }
    }
}
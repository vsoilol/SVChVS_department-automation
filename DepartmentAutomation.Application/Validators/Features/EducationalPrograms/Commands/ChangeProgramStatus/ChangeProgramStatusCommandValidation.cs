using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.EducationalPrograms.Commands.ChangeProgramStatus;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.EducationalPrograms.Commands.ChangeProgramStatus
{
    public class ChangeProgramStatusCommandValidation : AbstractValidator<ChangeProgramStatusCommand>
    {
        public ChangeProgramStatusCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Literatures.Commands.CreateLiterature;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Literatures.Commands.CreateLiterature
{
    public class CreateLiteratureCommandValidation : AbstractValidator<CreateLiteratureCommand>
    {
        public CreateLiteratureCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
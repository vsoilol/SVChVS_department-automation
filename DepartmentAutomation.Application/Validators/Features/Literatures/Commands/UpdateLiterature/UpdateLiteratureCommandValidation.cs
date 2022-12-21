using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Literatures.Commands.UpdateLiterature;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities.LiteratureInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Literatures.Commands.UpdateLiterature
{
    public class UpdateLiteratureCommandValidation : AbstractValidator<UpdateLiteratureCommand>
    {
        public UpdateLiteratureCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Literature>(context));
        }
    }
}
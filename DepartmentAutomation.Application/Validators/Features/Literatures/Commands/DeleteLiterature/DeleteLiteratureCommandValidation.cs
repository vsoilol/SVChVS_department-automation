using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Literatures.Commands.DeleteLiterature;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities.LiteratureInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Literatures.Commands.DeleteLiterature
{
    public class DeleteLiteratureCommandValidation : AbstractValidator<DeleteLiteratureCommand>
    {
        public DeleteLiteratureCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.LiteratureId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Literature>(context));
        }
    }
}
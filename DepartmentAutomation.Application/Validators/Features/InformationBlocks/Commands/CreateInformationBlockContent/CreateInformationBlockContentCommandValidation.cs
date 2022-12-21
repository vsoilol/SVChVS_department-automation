using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.InformationBlocks.Commands.CreateInformationBlockContent;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.InformationBlocks.Commands.CreateInformationBlockContent
{
    public class CreateInformationBlockContentCommandValidation : AbstractValidator<CreateInformationBlockContentCommand>
    {
        public CreateInformationBlockContentCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));

            RuleFor(x => x.InformationBlockId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<InformationBlock>(context));
        }
    }
}
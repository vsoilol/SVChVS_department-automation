using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetTemplatesByBlockId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities.InformationBlockInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.InformationBlocks.Queries.GetTemplatesByBlockId
{
    public class GetTemplatesByBlockIdQueryValidation : AbstractValidator<GetTemplatesByBlockIdQuery>
    {
        public GetTemplatesByBlockIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.InformationBlockId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<InformationBlock>(context));
        }
    }
}
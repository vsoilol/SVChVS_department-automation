using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.InformationBlocks.Queries.GetBlockByNumber;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.InformationBlocks.Queries.GetBlockByNumber
{
    public class GetBlockByNumberQueryValidation : AbstractValidator<GetBlockByNumberQuery>
    {
        public GetBlockByNumberQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
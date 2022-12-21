using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Indicators.Queries.GetIndicatorWithLevelsByProgramId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Indicators.Queries.GetIndicatorWithLevelsByProgramId
{
    public class GetIndicatorWithLevelsByProgramIdQueryValidator : AbstractValidator<GetIndicatorWithLevelsByProgramIdQuery>
    {
        public GetIndicatorWithLevelsByProgramIdQueryValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Literatures.Queries.GetLiteraturesByType;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Literatures.Queries.GetLiteraturesByType
{
    public class GetLiteraturesByTypeQueryValidation : AbstractValidator<GetLiteraturesByTypeQuery>
    {
        public GetLiteraturesByTypeQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
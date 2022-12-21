using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.MethodicalRecommendations.Queries.GetMethodicalRecommendationsByProgramId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.MethodicalRecommendations.Queries.GetMethodicalRecommendationsByProgramId
{
    public class GetMethodicalRecommendationsByProgramIdQueryValidation : AbstractValidator<GetMethodicalRecommendationsByProgramIdQuery>
    {
        public GetMethodicalRecommendationsByProgramIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
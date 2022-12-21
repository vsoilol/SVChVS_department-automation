using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.MethodicalRecommendations.Commands.UpdateMethodicalRecommendation;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.MethodicalRecommendations.Commands.UpdateMethodicalRecommendation
{
    public class UpdateMethodicalRecommendationCommandValidation : AbstractValidator<UpdateMethodicalRecommendationCommand>
    {
        public UpdateMethodicalRecommendationCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<MethodicalRecommendation>(context));
        }
    }
}
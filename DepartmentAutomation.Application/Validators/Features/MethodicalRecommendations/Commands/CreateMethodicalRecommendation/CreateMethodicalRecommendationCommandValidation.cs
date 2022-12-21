using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.MethodicalRecommendations.Commands.CreateMethodicalRecommendation;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.MethodicalRecommendations.Commands.CreateMethodicalRecommendation
{
    public class CreateMethodicalRecommendationCommandValidation : AbstractValidator<CreateMethodicalRecommendationCommand>
    {
        public CreateMethodicalRecommendationCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
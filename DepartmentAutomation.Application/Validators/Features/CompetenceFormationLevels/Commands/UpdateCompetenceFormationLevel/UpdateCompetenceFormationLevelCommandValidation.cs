using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.CompetenceFormationLevels.Commands.UpdateCompetenceFormationLevel;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.CompetenceFormationLevels.Commands.UpdateCompetenceFormationLevel
{
    public class
        UpdateCompetenceFormationLevelCommandValidation : AbstractValidator<UpdateCompetenceFormationLevelCommand>
    {
        public UpdateCompetenceFormationLevelCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<CompetenceFormationLevel>(context));

            RuleForEach(x => x.EvaluationToolTypeIds)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EvaluationToolType>(context));
        }
    }
}
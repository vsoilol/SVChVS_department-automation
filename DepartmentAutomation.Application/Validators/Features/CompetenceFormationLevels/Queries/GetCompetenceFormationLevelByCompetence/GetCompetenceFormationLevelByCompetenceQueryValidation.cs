using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.CompetenceFormationLevels.Queries.GetCompetenceFormationLevelByCompetence;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.CompetenceInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.CompetenceFormationLevels.Queries.GetCompetenceFormationLevelByCompetence
{
    public class GetCompetenceFormationLevelByCompetenceQueryValidation : AbstractValidator<GetCompetenceFormationLevelByCompetenceQuery>
    {
        public GetCompetenceFormationLevelByCompetenceQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));

            RuleFor(x => x.CompetenceId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Competence>(context));
        }
    }
}
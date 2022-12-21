using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.Disciplines.Queries.GetBriefDisciplineInfo;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.Disciplines.Queries.GetBriefDisciplineInfo
{
    public class GetBriefDisciplineInfoQueryValidation : AbstractValidator<GetBriefDisciplineInfoQuery>
    {
        public GetBriefDisciplineInfoQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.DisciplineId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Discipline>(context));
        }
    }
}
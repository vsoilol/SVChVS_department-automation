using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.KnowledgeControlForms.Queries.GetKnowledgeControlFormsByWeekId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.KnowledgeControlForms.Queries.GetKnowledgeControlFormsByWeekId
{
    public class GetKnowledgeControlFormsByWeekIdQueryValidation : AbstractValidator<GetKnowledgeControlFormsByWeekIdQuery>
    {
        public GetKnowledgeControlFormsByWeekIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.WeekId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Week>(context));
        }
    }
}
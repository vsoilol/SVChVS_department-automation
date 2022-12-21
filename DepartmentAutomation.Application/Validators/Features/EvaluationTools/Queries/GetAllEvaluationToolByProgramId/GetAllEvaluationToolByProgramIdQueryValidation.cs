using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.EvaluationTools.Queries.GetAllEvaluationToolByProgramId;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.EvaluationTools.Queries.GetAllEvaluationToolByProgramId
{
    public class GetAllEvaluationToolByProgramIdQueryValidation : AbstractValidator<GetAllEvaluationToolByProgramIdQuery>
    {
        public GetAllEvaluationToolByProgramIdQueryValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
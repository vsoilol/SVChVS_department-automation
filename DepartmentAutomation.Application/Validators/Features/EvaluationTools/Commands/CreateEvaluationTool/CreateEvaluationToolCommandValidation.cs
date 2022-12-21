using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.EvaluationTools.Commands.CreateEvaluationTool;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using DepartmentAutomation.Domain.Entities.EvaluationToolInfo;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.EvaluationTools.Commands.CreateEvaluationTool
{
    public class CreateEvaluationToolCommandValidation : AbstractValidator<CreateEvaluationToolCommand>
    {
        public CreateEvaluationToolCommandValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));

            RuleFor(x => x.EvaluationToolTypeId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EvaluationToolType>(context));
        }
    }
}
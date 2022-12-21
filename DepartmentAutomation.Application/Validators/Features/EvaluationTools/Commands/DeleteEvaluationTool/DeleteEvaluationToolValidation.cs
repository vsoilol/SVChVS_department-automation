using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.EvaluationTools.Commands.DeleteEvaluationTool;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.EvaluationTools.Commands.DeleteEvaluationTool
{
    public class DeleteEvaluationToolValidation : AbstractValidator<DeleteEvaluationToolCommand>
    {
        public DeleteEvaluationToolValidation(IApplicationDbContext context)
        {
            RuleFor(x => x.EducationalProgramId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<EducationalProgram>(context));
        }
    }
}
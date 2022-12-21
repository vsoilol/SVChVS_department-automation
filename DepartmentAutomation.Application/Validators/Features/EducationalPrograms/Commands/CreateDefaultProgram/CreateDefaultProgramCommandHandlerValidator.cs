using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Features.EducationalPrograms.Commands.CreateDefaultProgram;
using DepartmentAutomation.Application.Validators.PropertyValidators;
using DepartmentAutomation.Domain.Entities;
using FluentValidation;

namespace DepartmentAutomation.Application.Validators.Features.EducationalPrograms.Commands.CreateDefaultProgram
{
    public class CreateDefaultProgramCommandHandlerValidator : AbstractValidator<CreateDefaultProgramCommand>
    {
        public CreateDefaultProgramCommandHandlerValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.DisciplineId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThanOrEqualTo(1)
                .SetValidator(new SqlIdValidatorFor<Discipline>(context));
        }
    }
}